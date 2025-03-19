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

/// <summary>Extension specifying the configuration for visiting request nodes.</summary>
public interface IVisitConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.VisitConfigExtension";

    /// <summary>Provider for the <see cref="IFieldPathVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IFieldPathVisitor<IVisitorState>> FieldPathVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldFunctionVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IFieldFunctionVisitor<IVisitorState>> FieldFunctionVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldArgumentVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IFieldArgumentVisitor<IVisitorState>> FieldArgumentVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IConstantVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IConstantVisitor<IVisitorState>> ConstantVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IInputVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IInputVisitor<IVisitorState>> InputVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFilterVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IFilterVisitor<IVisitorState>> FilterVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ISortVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, ISortVisitor<IVisitorState>> SortVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ISkipVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, ISkipVisitor<IVisitorState>> SkipVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ILimitVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, ILimitVisitor<IVisitorState>> LimitVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ICountVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, ICountVisitor<IVisitorState>> CountVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IParameterVisitor{TState}"/>.</summary>
    public Func<ISchemaConfig, IParameterVisitor<IVisitorState>> ParameterVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IRequestVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IRequestVisitor<IVisitorState>> RequestVisitorProvider { get; }
}