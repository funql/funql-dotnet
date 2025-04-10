using System.Globalization;
using System.Text.Json;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Filtering.Configs.Builders.Extensions;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Schemas;
using FunQL.Core.Schemas.Configs.Core.Extensions;
using FunQL.Core.Sorting.Configs.Builders.Extensions;
using FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Linq.Schemas.Configs.Linq.Builders.Extensions;
using FunQL.Linq.Schemas.Extensions;

// =================================
// ======= Prepare demo data =======
// =================================

// Using an in-memory list of LEGO sets for this example. The list is converted to an IQueryable<Set>, so FunQL can
// apply LINQ expressions (like Where() and OrderBy()) based on the 'filter' and 'sort' parameters. Note that normally
// you would use e.g. the EF Core DbSet<Set>, which implements IQueryable<Set>, to directly query a database
IQueryable<Set> sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, DateTime.Parse("2017-10-01", styles: DateTimeStyles.AdjustToUniversal)),
    new("LEGO Star Wars The Razor Crest", 599.99, DateTime.Parse("2022-10-03", styles: DateTimeStyles.AdjustToUniversal)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, DateTime.Parse("2021-11-01", styles: DateTimeStyles.AdjustToUniversal)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, DateTime.Parse("2018-09-01", styles: DateTimeStyles.AdjustToUniversal)),
}.AsQueryable();

// Filter result to only include sets where:
// - The uppercased name contains "STAR WARS"
// - The price is greater than or equal to 500
// - The launchTime's year is greater than 2010
const string filter = "and(has(upper(name), \"STAR WARS\"), gte(price, 500), gt(year(launchTime), 2010))";
// Sort result by price in descending order 
const string sort = "desc(price)";

// =================================
// ===== Execute FunQL request =====
// =================================

// Create the ApiSchema, which is the entry point for FunQL requests
var schema = new ApiSchema();

// Execute the listSets() FunQL request for the 'filter' and 'sort' parameters, which will run the following steps:
// - Parse the 'filter' and 'sort' parameters using the 'listSets' request configuration
// - Validate the parameters based on the 'listSets' request configuration
// - Translate the parameters to LINQ expressions
// - Apply the LINQ expressions to 'IQueryable<Set> sets'
// - Fetch the filtered and sorted result
var result = await sets
    .ExecuteRequestForParameters(schema, requestName: "listSets", filter: filter, sort: sort);

// Print the JSON result
var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(result.Data, jsonSerializerOptions));
// Output:
// [
//   {
//     "name": "LEGO Star Wars Millennium Falcon",
//     "price": 849.99,
//     "launchTime": "2017-10-01T00:00:00"
//   },
//   {
//     "name": "LEGO Star Wars The Razor Crest",
//     "price": 599.99,
//     "launchTime": "2022-10-03T00:00:00"
//   }
// ]


// =================================
// ====== FunQL configuration ======
// =================================

// Data model of a LEGO set for this example. This type is configured in ApiSchema, which FunQL uses when parsing,
// validating and executing FunQL requests
public sealed record Set(string Name, double Price, DateTime LaunchTime);

// FunQL schema configuration for this example. The FunQL Schema class is the entry point for FunQL requests, defining
// the configuration for e.g. parsing, validating and executing FunQL requests.
public sealed class ApiSchema : Schema
{
    // Override OnInitializeSchema() to configure the schema, adding features and configuring the supported FunQL
    // requests
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // ===== Features =====
        // Add all core features with their core configuration: Parse, Print, Validate, Visit and Execute
        schema.AddCoreFeatures();
        // Add LINQ features so FunQL can translate FunQL queries to LINQ expressions, e.g. for use with EFCore DbSet
        schema.AddLinqFeature();
        
        // ===== Requests =====
        // Add the 'listSets()' request and configure it
        schema.Request("listSets")
            // Enable support for the 'filter()' parameter
            .SupportsFilter()
            // Enable support for the 'sort()' parameter
            .SupportsSort()
            // Define the 'listSets()' return type to be a list of 'Set' objects. The return type is used to configure
            // which fields can be filtered/sorted on
            .ReturnsListOfObjects<Set>(set =>
            {
                // Configure the 'Set.Name' property so it's recognized by FunQL 
                // Use '.SimpleField()' for simple types (1, "value", true, etc.)
                // Use '.ObjectField()' for object types ({ Name: "value" })
                // Use '.ListField()' for list types ([ "value" ])
                set.SimpleField(it => it.Name)
                    // By default, the property's name ('Name') is used, but in JSON it's called 'name', so override it
                    // so FunQL uses the JSON name
                    .HasName("name")
                    // Enable support for filtering on this field
                    // Using '.SupportsStringFilterFunctions()' enables support for:
                    // - eq, neq
                    // - gt, gte, lt, lte
                    // - has, stw, enw, reg
                    // - lower, upper
                    .SupportsFilter(it => it.SupportsStringFilterFunctions())
                    // Enable support for sorting on this field
                    // Using '.SupportsStringFieldFunctions()' enables support for:
                    // - lower, upper
                    .SupportsSort(it => it.SupportsStringFieldFunctions());
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    // Using '.SupportsDoubleFilterFunctions()' enables support for:
                    // - eq, neq
                    // - gt, gte, lt, lte
                    // - floor, ceil, round
                    .SupportsFilter(it => it.SupportsDoubleFilterFunctions())
                    // Using '.SupportsDoubleFieldFunctions()' enables support for:
                    // - floor, ceil, round
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());
                set.SimpleField(it => it.LaunchTime)
                    .HasName("launchTime")
                    // Using '.SupportsDateTimeFilterFunctions()' enables support for:
                    // - eq, neq
                    // - gt, gte, lt, lte
                    // - year, month, day, hour, minute, second, millisecond
                    .SupportsFilter(it => it.SupportsDateTimeFilterFunctions())
                    // Using '.SupportsDateTimeFieldFunctions()' enables support for:
                    // - year, month, day, hour, minute, second, millisecond
                    .SupportsSort(it => it.SupportsDateTimeFieldFunctions());
            });
    }
}