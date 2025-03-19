// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Counting.Parsers;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Filtering.Parsers;
using FunQL.Core.Inputting.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Limiting.Parsers;
using FunQL.Core.Requests.Parsers;
using FunQL.Core.Schemas.Configs.Json.Extensions;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;
using FunQL.Core.Skipping.Parsers;
using FunQL.Core.Sorting.Parsers;

namespace FunQL.Core.Schemas.Configs.Parse;

/// <summary>
/// Default implementation of <see cref="IMutableParseConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
///   - <see cref="ParserStateFactory"/> will enter <see cref="SchemaConfigParseContext"/>
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableParseConfigExtension : MutableConfigExtension, IMutableParseConfigExtension
{
    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutableParseConfigExtension(string name) : base(name)
    {
        IFieldPathParser? fieldPathParser = null;
        FieldPathParserProvider = _ => fieldPathParser ??= new FieldPathParser();

        IFieldFunctionParser? fieldFunctionParser = null;
        FieldFunctionParserProvider = schemaConfig => fieldFunctionParser ??= new FieldFunctionParser(
            FieldPathParserProvider(schemaConfig)
        );

        IFieldArgumentParser? fieldArgumentParser = null;
        FieldArgumentParserProvider = schemaConfig => fieldArgumentParser ??= new FieldArgumentParser(
            FieldFunctionParserProvider(schemaConfig)
        );

        IConstantParser? constantParser = null;
        ConstantParserProvider = schemaConfig => constantParser ??= new JsonConstantParser(
            schemaConfig.FindJsonConfigExtension()?.JsonSerializerOptions ?? JsonSerializerOptions.Default
        );

        IInputConstantParser? inputConstantParser = null;
        InputConstantParserProvider = schemaConfig => inputConstantParser ??= new ConfigInputConstantParser(
            ConstantParserProvider(schemaConfig)
        );

        IInputParser? inputParser = null;
        InputParserProvider = schemaConfig => inputParser ??= new InputParser(
            InputConstantParserProvider(schemaConfig)
        );

        IFilterConstantParser? filterConstantParser = null;
        FilterConstantParserProvider = schemaConfig => filterConstantParser ??= new ConfigFilterConstantParser(
            ConstantParserProvider(schemaConfig)
        );

        IFilterParser? filterParser = null;
        FilterParserProvider = schemaConfig => filterParser ??= new FilterParser(
            FieldArgumentParserProvider(schemaConfig),
            FilterConstantParserProvider(schemaConfig)
        );

        ISortParser? sortParser = null;
        SortParserProvider = schemaConfig => sortParser ??= new SortParser(FieldArgumentParserProvider(schemaConfig));

        ISkipParser? skipParser = null;
        SkipParserProvider = schemaConfig => skipParser ??= new SkipParser(ConstantParserProvider(schemaConfig));

        ILimitParser? limitParser = null;
        LimitParserProvider = schemaConfig => limitParser ??= new LimitParser(ConstantParserProvider(schemaConfig));

        ICountParser? countParser = null;
        CountParserProvider = schemaConfig => countParser ??= new CountParser(ConstantParserProvider(schemaConfig));

        IParameterParser? parameterParser = null;
        ParameterParserProvider = schemaConfig => parameterParser ??= new ParameterParser(
            InputParserProvider(schemaConfig),
            FilterParserProvider(schemaConfig),
            SortParserProvider(schemaConfig),
            SkipParserProvider(schemaConfig),
            LimitParserProvider(schemaConfig),
            CountParserProvider(schemaConfig)
        );

        IRequestParser? requestParser = null;
        RequestParserProvider = schemaConfig => requestParser ??= new RequestParser(
            ParameterParserProvider(schemaConfig)
        );

        ParserStateFactory = (schemaConfig, lexer) =>
        {
            var state = new ParserState(lexer);
            // Don't have to explicitly exit this context as we're not inside a parser
            state.EnterContext(new SchemaConfigParseContext(schemaConfig));
            return state;
        };
    }

    /// <inheritdoc cref="IMutableParseConfigExtension.FieldPathParserProvider"/>
    public Func<ISchemaConfig, IFieldPathParser> FieldPathParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.FieldFunctionParserProvider"/>
    public Func<ISchemaConfig, IFieldFunctionParser> FieldFunctionParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.FieldArgumentParserProvider"/>
    public Func<ISchemaConfig, IFieldArgumentParser> FieldArgumentParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.ConstantParserProvider"/>
    public Func<ISchemaConfig, IConstantParser> ConstantParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.InputConstantParserProvider"/>
    public Func<ISchemaConfig, IInputConstantParser> InputConstantParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.InputParserProvider"/>
    public Func<ISchemaConfig, IInputParser> InputParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.FilterConstantParserProvider"/>
    public Func<ISchemaConfig, IFilterConstantParser> FilterConstantParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.FilterParserProvider"/>
    public Func<ISchemaConfig, IFilterParser> FilterParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.SortParserProvider"/>
    public Func<ISchemaConfig, ISortParser> SortParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.SkipParserProvider"/>
    public Func<ISchemaConfig, ISkipParser> SkipParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.LimitParserProvider"/>
    public Func<ISchemaConfig, ILimitParser> LimitParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.CountParserProvider"/>
    public Func<ISchemaConfig, ICountParser> CountParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.ParameterParserProvider"/>
    public Func<ISchemaConfig, IParameterParser> ParameterParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.RequestParserProvider"/>
    public Func<ISchemaConfig, IRequestParser> RequestParserProvider { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.ParserStateFactory"/>
    public Func<ISchemaConfig, ILexer, IParserState> ParserStateFactory { get; set; }

    /// <inheritdoc cref="IMutableParseConfigExtension.ToConfig"/>
    public override IParseConfigExtension ToConfig() => new ImmutableParseConfigExtension(
        Name, FieldPathParserProvider, FieldFunctionParserProvider, FieldArgumentParserProvider, ConstantParserProvider,
        InputConstantParserProvider, InputParserProvider, FilterConstantParserProvider, FilterParserProvider,
        SortParserProvider, SkipParserProvider, LimitParserProvider, CountParserProvider, ParameterParserProvider,
        RequestParserProvider, ParserStateFactory
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}