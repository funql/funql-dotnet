// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableValidateConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/> and returns <see cref="IValidateConfigBuilder"/> to
    /// configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableValidateConfigExtension"/> for.</param>
    /// <param name="addVisitFeature">
    /// Whether to add the <see cref="IVisitConfigExtension"/> as well. This is required for the default implementation
    /// of <see cref="Validator"/>. Default <c>true</c>.
    /// </param>
    /// <param name="withCoreRules">
    /// Whether to add all core <see cref="IValidationRule"/> as well. Default <c>true</c>.
    /// </param>
    /// <returns>
    /// The <see cref="IValidateConfigBuilder"/> to build <see cref="IMutableValidateConfigExtension"/>.
    /// </returns>
    public static IValidateConfigBuilder ValidateFeature(
        this ISchemaConfigBuilder builder,
        bool addVisitFeature = true,
        bool withCoreRules = true
    )
    {
        if (addVisitFeature)
            builder.VisitFeature();

        const string name = IValidateConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableValidateConfigExtension>(name);
        if (config == null)
        {
            config = new MutableValidateConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        var validateBuilder = new ValidateConfigBuilder(config);

        if (withCoreRules)
            validateBuilder.WithCoreValidationRules();

        return validateBuilder;
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableValidateConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/>, calling the <paramref name="action"/> to configure it and
    /// returns <see cref="ISchemaConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableValidateConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableValidateConfigExtension"/>.</param>
    /// <param name="addVisitFeature">
    /// Whether to add the <see cref="IVisitConfigExtension"/> as well. This is required for the default implementation
    /// of <see cref="Validator"/>. Default <c>true</c>.
    /// </param>
    /// <param name="withCoreRules">
    /// Whether to add all core <see cref="IValidationRule"/> as well. Default <c>true</c>.
    /// </param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddValidateFeature(
        this ISchemaConfigBuilder builder,
        Action<IValidateConfigBuilder>? action = null,
        bool addVisitFeature = true,
        bool withCoreRules = true
    )
    {
        var nestedBuilder = ValidateFeature(builder, addVisitFeature, withCoreRules);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}