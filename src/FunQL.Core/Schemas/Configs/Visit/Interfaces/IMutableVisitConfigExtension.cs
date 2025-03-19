// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
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
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Schemas.Configs.Visit.Interfaces;

/// <summary>Mutable version of <see cref="IVisitConfigExtension"/>.</summary>
public interface IMutableVisitConfigExtension : IVisitConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IVisitConfigExtension.FieldPathVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldPathVisitor<IVisitorState>> FieldPathVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.FieldFunctionVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldFunctionVisitor<IVisitorState>> FieldFunctionVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.FieldArgumentVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldArgumentVisitor<IVisitorState>> FieldArgumentVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.ConstantVisitorProvider"/>
    public new Func<ISchemaConfig, IConstantVisitor<IVisitorState>> ConstantVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.InputVisitorProvider"/>
    public new Func<ISchemaConfig, IInputVisitor<IVisitorState>> InputVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.FilterVisitorProvider"/>
    public new Func<ISchemaConfig, IFilterVisitor<IVisitorState>> FilterVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.SortVisitorProvider"/>
    public new Func<ISchemaConfig, ISortVisitor<IVisitorState>> SortVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.SkipVisitorProvider"/>
    public new Func<ISchemaConfig, ISkipVisitor<IVisitorState>> SkipVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.LimitVisitorProvider"/>
    public new Func<ISchemaConfig, ILimitVisitor<IVisitorState>> LimitVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.CountVisitorProvider"/>
    public new Func<ISchemaConfig, ICountVisitor<IVisitorState>> CountVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.ParameterVisitorProvider"/>
    public new Func<ISchemaConfig, IParameterVisitor<IVisitorState>> ParameterVisitorProvider { get; set; }

    /// <inheritdoc cref="IVisitConfigExtension.RequestVisitorProvider"/>
    public new Func<ISchemaConfig, IRequestVisitor<IVisitorState>> RequestVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IVisitConfigExtension ToConfig();
}