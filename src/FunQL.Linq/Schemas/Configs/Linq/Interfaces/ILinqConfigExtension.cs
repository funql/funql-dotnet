// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Constants.Visitors;
using FunQL.Linq.Fields.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Fields.Visitors.Functions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using FunQL.Linq.Filtering.Visitors;
using FunQL.Linq.Filtering.Visitors.Translators;
using FunQL.Linq.Sorting.Visitors;

namespace FunQL.Linq.Schemas.Configs.Linq.Interfaces;

/// <summary>Extension specifying the configuration for translating FunQL to LINQ.</summary>
public interface ILinqConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Linq.LinqConfigExtension";

    /// <summary>
    /// Whether to handle null propagation. If <c>true</c>, null-checks are included to ensure safe handling of null
    /// values within expressions.
    /// </summary>
    public bool HandleNullPropagation { get; }

    /// <summary>Provider for the <see cref="IFieldPathLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldPathLinqVisitor<ILinqVisitorState>> FieldPathLinqVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldFunctionLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldFunctionLinqVisitor<ILinqVisitorState>> FieldFunctionLinqVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFieldArgumentLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFieldArgumentLinqVisitor<ILinqVisitorState>> FieldArgumentLinqVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IConstantLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IConstantLinqVisitor<ILinqVisitorState>> ConstantLinqVisitorProvider { get; }

    /// <summary>Provider for the <see cref="IFilterLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, IFilterLinqVisitor<ILinqVisitorState>> FilterLinqVisitorProvider { get; }

    /// <summary>Provider for the <see cref="ISortLinqVisitor{T}"/>.</summary>
    public Func<ISchemaConfig, ISortLinqVisitor<ISortLinqVisitorState>> SortLinqVisitorProvider { get; }

    /// <summary>Factory to create the <see cref="ILinqVisitorState"/> for given <see cref="IRequestConfig"/>.</summary>
    public Func<ISchemaConfig, IRequestConfig, ILinqVisitorState> LinqVisitorStateFactory { get; }

    /// <summary>
    /// Factory to create the <see cref="ISortLinqVisitorState"/> for given <see cref="IRequestConfig"/>.
    /// </summary>
    public Func<ISchemaConfig, IRequestConfig, ISortLinqVisitorState> SortLinqVisitorStateFactory { get; }

    /// <summary>
    /// The list of <see cref="IFieldFunctionLinqTranslator"/> to use for translating <see cref="FieldFunction"/> to
    /// LINQ.
    /// </summary>
    public IEnumerable<IFieldFunctionLinqTranslator> GetFieldFunctionLinqTranslators();

    /// <summary>
    /// The list of <see cref="IBinaryExpressionLinqTranslator"/> to use for translating binary
    /// <see cref="BooleanExpression"/> to LINQ.
    /// </summary>
    public IEnumerable<IBinaryExpressionLinqTranslator> GetBinaryExpressionLinqTranslators();
}