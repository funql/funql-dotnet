// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Visitors;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Counting.Visitors;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Core.Filtering.Visitors;
using FunQL.Core.Inputting.Visitors;
using FunQL.Core.Limiting.Visitors;
using FunQL.Core.Requests.Visitors;
using FunQL.Core.Schemas.Configs.Json.Extensions;
using FunQL.Core.Schemas.Configs.Print.Interfaces;
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Schemas.Configs.Print;

/// <summary>
/// Default implementation of <see cref="IMutablePrintConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
///   - <see cref="PrintVisitorStateFactory"/> will use <see cref="StringWriter"/>
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutablePrintConfigExtension : MutableConfigExtension, IMutablePrintConfigExtension
{
    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutablePrintConfigExtension(string name) : base(name)
    {
        IFieldPathPrintVisitor<IPrintVisitorState>? fieldPathPrintVisitor = null;
        FieldPathPrintVisitorProvider = _ => fieldPathPrintVisitor ??= new FieldPathPrintVisitor<IPrintVisitorState>();

        IFieldFunctionPrintVisitor<IPrintVisitorState>? fieldFunctionPrintVisitor = null;
        FieldFunctionPrintVisitorProvider = schemaConfig =>
            fieldFunctionPrintVisitor ??= new FieldFunctionPrintVisitor<IPrintVisitorState>(
                FieldPathPrintVisitorProvider(schemaConfig)
            );

        IFieldArgumentPrintVisitor<IPrintVisitorState>? fieldArgumentPrintVisitor = null;
        FieldArgumentPrintVisitorProvider = schemaConfig =>
            fieldArgumentPrintVisitor ??= new FieldArgumentPrintVisitor<IPrintVisitorState>(
                FieldFunctionPrintVisitorProvider(schemaConfig)
            );

        IConstantPrintVisitor<IPrintVisitorState>? constantPrintVisitor = null;
        ConstantPrintVisitorProvider = schemaConfig =>
            constantPrintVisitor ??= new JsonConstantPrintVisitor<IPrintVisitorState>(
                schemaConfig.FindJsonConfigExtension()?.JsonSerializerOptions ?? JsonSerializerOptions.Default
            );

        IInputPrintVisitor<IPrintVisitorState>? inputPrintVisitor = null;
        InputPrintVisitorProvider = schemaConfig => inputPrintVisitor ??= new InputPrintVisitor<IPrintVisitorState>(
            ConstantPrintVisitorProvider(schemaConfig)
        );

        IFilterPrintVisitor<IPrintVisitorState>? filterPrintVisitor = null;
        FilterPrintVisitorProvider = schemaConfig => filterPrintVisitor ??= new FilterPrintVisitor<IPrintVisitorState>(
            FieldArgumentPrintVisitorProvider(schemaConfig),
            ConstantPrintVisitorProvider(schemaConfig)
        );

        ISortPrintVisitor<IPrintVisitorState>? sortPrintVisitor = null;
        SortPrintVisitorProvider = schemaConfig => sortPrintVisitor ??= new SortPrintVisitor<IPrintVisitorState>(
            FieldArgumentPrintVisitorProvider(schemaConfig)
        );

        ISkipPrintVisitor<IPrintVisitorState>? skipPrintVisitor = null;
        SkipPrintVisitorProvider = schemaConfig => skipPrintVisitor ??= new SkipPrintVisitor<IPrintVisitorState>(
            ConstantPrintVisitorProvider(schemaConfig)
        );

        ILimitPrintVisitor<IPrintVisitorState>? limitPrintVisitor = null;
        LimitPrintVisitorProvider = schemaConfig => limitPrintVisitor ??= new LimitPrintVisitor<IPrintVisitorState>(
            ConstantPrintVisitorProvider(schemaConfig)
        );

        ICountPrintVisitor<IPrintVisitorState>? countPrintVisitor = null;
        CountPrintVisitorProvider = schemaConfig => countPrintVisitor ??= new CountPrintVisitor<IPrintVisitorState>(
            ConstantPrintVisitorProvider(schemaConfig)
        );

        IParameterPrintVisitor<IPrintVisitorState>? parameterPrintVisitor = null;
        ParameterPrintVisitorProvider = schemaConfig =>
            parameterPrintVisitor ??= new ParameterPrintVisitor<IPrintVisitorState>(
                InputPrintVisitorProvider(schemaConfig),
                FilterPrintVisitorProvider(schemaConfig),
                SortPrintVisitorProvider(schemaConfig),
                SkipPrintVisitorProvider(schemaConfig),
                LimitPrintVisitorProvider(schemaConfig),
                CountPrintVisitorProvider(schemaConfig)
            );

        IRequestPrintVisitor<IPrintVisitorState>? requestPrintVisitor = null;
        RequestPrintVisitorProvider =
            schemaConfig => requestPrintVisitor ??= new RequestPrintVisitor<IPrintVisitorState>(
                ParameterPrintVisitorProvider(schemaConfig)
            );

        PrintVisitorStateFactory = _ =>
        {
            var state = new PrintVisitorState(new StringWriter());
            return state;
        };
    }

    /// <inheritdoc cref="IMutablePrintConfigExtension.FieldPathPrintVisitorProvider"/>
    public Func<ISchemaConfig, IFieldPathPrintVisitor<IPrintVisitorState>> FieldPathPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.FieldFunctionPrintVisitorProvider"/>
    public Func<ISchemaConfig, IFieldFunctionPrintVisitor<IPrintVisitorState>>
        FieldFunctionPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.FieldArgumentPrintVisitorProvider"/>
    public Func<ISchemaConfig, IFieldArgumentPrintVisitor<IPrintVisitorState>>
        FieldArgumentPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.ConstantPrintVisitorProvider"/>
    public Func<ISchemaConfig, IConstantPrintVisitor<IPrintVisitorState>> ConstantPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.InputPrintVisitorProvider"/>
    public Func<ISchemaConfig, IInputPrintVisitor<IPrintVisitorState>> InputPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.FilterPrintVisitorProvider"/>
    public Func<ISchemaConfig, IFilterPrintVisitor<IPrintVisitorState>> FilterPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.SortPrintVisitorProvider"/>
    public Func<ISchemaConfig, ISortPrintVisitor<IPrintVisitorState>> SortPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.SkipPrintVisitorProvider"/>
    public Func<ISchemaConfig, ISkipPrintVisitor<IPrintVisitorState>> SkipPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.LimitPrintVisitorProvider"/>
    public Func<ISchemaConfig, ILimitPrintVisitor<IPrintVisitorState>> LimitPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.CountPrintVisitorProvider"/>
    public Func<ISchemaConfig, ICountPrintVisitor<IPrintVisitorState>> CountPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.ParameterPrintVisitorProvider"/>
    public Func<ISchemaConfig, IParameterPrintVisitor<IPrintVisitorState>> ParameterPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.RequestPrintVisitorProvider"/>
    public Func<ISchemaConfig, IRequestPrintVisitor<IPrintVisitorState>> RequestPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.PrintVisitorStateFactory"/>
    public Func<ISchemaConfig, IPrintVisitorState> PrintVisitorStateFactory { get; set; }

    /// <inheritdoc cref="IMutablePrintConfigExtension.ToConfig"/>
    public override IPrintConfigExtension ToConfig() => new ImmutablePrintConfigExtension(
        Name, FieldPathPrintVisitorProvider, FieldFunctionPrintVisitorProvider, FieldArgumentPrintVisitorProvider,
        ConstantPrintVisitorProvider, InputPrintVisitorProvider, FilterPrintVisitorProvider, SortPrintVisitorProvider,
        SkipPrintVisitorProvider, LimitPrintVisitorProvider, CountPrintVisitorProvider, ParameterPrintVisitorProvider,
        RequestPrintVisitorProvider, PrintVisitorStateFactory
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}