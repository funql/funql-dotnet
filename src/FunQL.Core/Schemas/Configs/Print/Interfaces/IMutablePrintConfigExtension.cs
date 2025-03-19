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

namespace FunQL.Core.Schemas.Configs.Print.Interfaces;

/// <summary>Mutable version of <see cref="IPrintConfigExtension"/>.</summary>
public interface IMutablePrintConfigExtension : IPrintConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IPrintConfigExtension.FieldPathPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldPathPrintVisitor<IPrintVisitorState>>
        FieldPathPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.FieldFunctionPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldFunctionPrintVisitor<IPrintVisitorState>>
        FieldFunctionPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.FieldArgumentPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldArgumentPrintVisitor<IPrintVisitorState>>
        FieldArgumentPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.ConstantPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IConstantPrintVisitor<IPrintVisitorState>> ConstantPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.InputPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IInputPrintVisitor<IPrintVisitorState>> InputPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.FilterPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IFilterPrintVisitor<IPrintVisitorState>> FilterPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.SortPrintVisitorProvider"/>
    public new Func<ISchemaConfig, ISortPrintVisitor<IPrintVisitorState>> SortPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.SkipPrintVisitorProvider"/>
    public new Func<ISchemaConfig, ISkipPrintVisitor<IPrintVisitorState>> SkipPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.LimitPrintVisitorProvider"/>
    public new Func<ISchemaConfig, ILimitPrintVisitor<IPrintVisitorState>> LimitPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.CountPrintVisitorProvider"/>
    public new Func<ISchemaConfig, ICountPrintVisitor<IPrintVisitorState>> CountPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.ParameterPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IParameterPrintVisitor<IPrintVisitorState>>
        ParameterPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.RequestPrintVisitorProvider"/>
    public new Func<ISchemaConfig, IRequestPrintVisitor<IPrintVisitorState>> RequestPrintVisitorProvider { get; set; }

    /// <inheritdoc cref="IPrintConfigExtension.PrintVisitorStateFactory"/>
    public new Func<ISchemaConfig, IPrintVisitorState> PrintVisitorStateFactory { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IPrintConfigExtension ToConfig();
}