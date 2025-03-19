// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

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
using FunQL.Core.Schemas.Configs.Visit.Interfaces;
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Schemas.Configs.Visit;

/// <summary>
/// Default implementation of <see cref="IMutableVisitConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableVisitConfigExtension : MutableConfigExtension, IMutableVisitConfigExtension
{
    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutableVisitConfigExtension(string name) : base(name)
    {
        IFieldPathVisitor<IVisitorState>? fieldPathVisitor = null;
        FieldPathVisitorProvider = _ => fieldPathVisitor ??= new FieldPathVisitor<IVisitorState>();

        IFieldFunctionVisitor<IVisitorState>? fieldFunctionVisitor = null;
        FieldFunctionVisitorProvider = schemaConfig => fieldFunctionVisitor ??= new FieldFunctionVisitor<IVisitorState>(
            FieldPathVisitorProvider(schemaConfig)
        );

        IFieldArgumentVisitor<IVisitorState>? fieldArgumentVisitor = null;
        FieldArgumentVisitorProvider = schemaConfig => fieldArgumentVisitor ??= new FieldArgumentVisitor<IVisitorState>(
            FieldFunctionVisitorProvider(schemaConfig)
        );

        IConstantVisitor<IVisitorState>? constantVisitor = null;
        ConstantVisitorProvider = _ => constantVisitor ??= new ConstantVisitor<IVisitorState>();

        IInputVisitor<IVisitorState>? inputVisitor = null;
        InputVisitorProvider = schemaConfig => inputVisitor ??= new InputVisitor<IVisitorState>(
            ConstantVisitorProvider(schemaConfig)
        );

        IFilterVisitor<IVisitorState>? filterVisitor = null;
        FilterVisitorProvider = schemaConfig => filterVisitor ??= new FilterVisitor<IVisitorState>(
            FieldArgumentVisitorProvider(schemaConfig),
            ConstantVisitorProvider(schemaConfig)
        );

        ISortVisitor<IVisitorState>? sortVisitor = null;
        SortVisitorProvider = schemaConfig => sortVisitor ??= new SortVisitor<IVisitorState>(
            FieldArgumentVisitorProvider(schemaConfig)
        );

        ISkipVisitor<IVisitorState>? skipVisitor = null;
        SkipVisitorProvider = schemaConfig => skipVisitor ??= new SkipVisitor<IVisitorState>(
            ConstantVisitorProvider(schemaConfig)
        );

        ILimitVisitor<IVisitorState>? limitVisitor = null;
        LimitVisitorProvider = schemaConfig => limitVisitor ??= new LimitVisitor<IVisitorState>(
            ConstantVisitorProvider(schemaConfig)
        );

        ICountVisitor<IVisitorState>? countVisitor = null;
        CountVisitorProvider = schemaConfig => countVisitor ??= new CountVisitor<IVisitorState>(
            ConstantVisitorProvider(schemaConfig)
        );

        IParameterVisitor<IVisitorState>? parameterVisitor = null;
        ParameterVisitorProvider = schemaConfig => parameterVisitor ??= new ParameterVisitor<IVisitorState>(
            InputVisitorProvider(schemaConfig),
            FilterVisitorProvider(schemaConfig),
            SortVisitorProvider(schemaConfig),
            SkipVisitorProvider(schemaConfig),
            LimitVisitorProvider(schemaConfig),
            CountVisitorProvider(schemaConfig)
        );

        IRequestVisitor<IVisitorState>? requestVisitor = null;
        RequestVisitorProvider = schemaConfig => requestVisitor ??= new RequestVisitor<IVisitorState>(
            ParameterVisitorProvider(schemaConfig)
        );
    }

    /// <inheritdoc cref="IMutableVisitConfigExtension.FieldPathVisitorProvider"/>
    public Func<ISchemaConfig, IFieldPathVisitor<IVisitorState>> FieldPathVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.FieldFunctionVisitorProvider"/>
    public Func<ISchemaConfig, IFieldFunctionVisitor<IVisitorState>>
        FieldFunctionVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.FieldArgumentVisitorProvider"/>
    public Func<ISchemaConfig, IFieldArgumentVisitor<IVisitorState>>
        FieldArgumentVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.ConstantVisitorProvider"/>
    public Func<ISchemaConfig, IConstantVisitor<IVisitorState>> ConstantVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.InputVisitorProvider"/>
    public Func<ISchemaConfig, IInputVisitor<IVisitorState>> InputVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.FilterVisitorProvider"/>
    public Func<ISchemaConfig, IFilterVisitor<IVisitorState>> FilterVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.SortVisitorProvider"/>
    public Func<ISchemaConfig, ISortVisitor<IVisitorState>> SortVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.SkipVisitorProvider"/>
    public Func<ISchemaConfig, ISkipVisitor<IVisitorState>> SkipVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.LimitVisitorProvider"/>
    public Func<ISchemaConfig, ILimitVisitor<IVisitorState>> LimitVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.CountVisitorProvider"/>
    public Func<ISchemaConfig, ICountVisitor<IVisitorState>> CountVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.ParameterVisitorProvider"/>
    public Func<ISchemaConfig, IParameterVisitor<IVisitorState>> ParameterVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.RequestVisitorProvider"/>
    public Func<ISchemaConfig, IRequestVisitor<IVisitorState>> RequestVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableVisitConfigExtension.ToConfig"/>
    public override IVisitConfigExtension ToConfig() => new ImmutableVisitConfigExtension(
        Name, FieldPathVisitorProvider, FieldFunctionVisitorProvider, FieldArgumentVisitorProvider,
        ConstantVisitorProvider, InputVisitorProvider, FilterVisitorProvider, SortVisitorProvider, SkipVisitorProvider,
        LimitVisitorProvider, CountVisitorProvider, ParameterVisitorProvider, RequestVisitorProvider
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}