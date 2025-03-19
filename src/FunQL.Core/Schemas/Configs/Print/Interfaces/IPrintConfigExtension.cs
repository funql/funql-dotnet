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

/// <summary>Extension specifying the configuration for printing requests.</summary>
public interface IPrintConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.PrintConfigExtension";

    /// <summary>Provider for the <see cref="IFieldPathPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldPathPrintVisitor<IPrintVisitorState>> FieldPathPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldFunctionPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldFunctionPrintVisitor<IPrintVisitorState>>
        FieldFunctionPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldArgumentPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldArgumentPrintVisitor<IPrintVisitorState>>
        FieldArgumentPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IConstantPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IConstantPrintVisitor<IPrintVisitorState>> ConstantPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IInputPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IInputPrintVisitor<IPrintVisitorState>> InputPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFilterPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFilterPrintVisitor<IPrintVisitorState>> FilterPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ISortPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, ISortPrintVisitor<IPrintVisitorState>> SortPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ISkipPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, ISkipPrintVisitor<IPrintVisitorState>> SkipPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ILimitPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, ILimitPrintVisitor<IPrintVisitorState>> LimitPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ICountPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, ICountPrintVisitor<IPrintVisitorState>> CountPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IParameterPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IParameterPrintVisitor<IPrintVisitorState>> ParameterPrintVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IRequestPrintVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IRequestPrintVisitor<IPrintVisitorState>> RequestPrintVisitorProvider { get; }

    /// <summary>Factory to create the <see cref="IPrintVisitorState"/>.</summary>
    public Func<ISchemaConfig, IPrintVisitorState> PrintVisitorStateFactory { get; }
}