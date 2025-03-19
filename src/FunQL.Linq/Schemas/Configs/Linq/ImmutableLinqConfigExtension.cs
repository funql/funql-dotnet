// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Constants.Visitors;
using FunQL.Linq.Fields.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Fields.Visitors.Functions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using FunQL.Linq.Filtering.Visitors;
using FunQL.Linq.Filtering.Visitors.Translators;
using FunQL.Linq.Schemas.Configs.Linq.Interfaces;
using FunQL.Linq.Sorting.Visitors;

namespace FunQL.Linq.Schemas.Configs.Linq;

/// <summary>Immutable implementation of <see cref="ILinqConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="HandleNullPropagation">The <see cref="ILinqConfigExtension.HandleNullPropagation"/>.</param>
/// <param name="FieldPathLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.FieldPathLinqVisitorProvider"/>.
/// </param>
/// <param name="FieldFunctionLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.FieldFunctionLinqVisitorProvider"/>.
/// </param>
/// <param name="FieldArgumentLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.FieldArgumentLinqVisitorProvider"/>.
/// </param>
/// <param name="ConstantLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.ConstantLinqVisitorProvider"/>.
/// </param>
/// <param name="FilterLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.FilterLinqVisitorProvider"/>.
/// </param>
/// <param name="SortLinqVisitorProvider">
/// The <see cref="ILinqConfigExtension.SortLinqVisitorProvider"/>.
/// </param>
/// <param name="LinqVisitorStateFactory">
/// The <see cref="ILinqConfigExtension.LinqVisitorStateFactory"/>.
/// </param>
/// <param name="SortLinqVisitorStateFactory">
/// The <see cref="ILinqConfigExtension.SortLinqVisitorStateFactory"/>.
/// </param>
/// <param name="FieldFunctionLinqTranslators">
/// The <see cref="ILinqConfigExtension.GetFieldFunctionLinqTranslators"/>.
/// </param>
/// <param name="BinaryExpressionLinqTranslators">
/// The <see cref="ILinqConfigExtension.GetBinaryExpressionLinqTranslators"/>.
/// </param>
public sealed record ImmutableLinqConfigExtension(
    string Name,
    bool HandleNullPropagation,
    Func<ISchemaConfig, IFieldPathLinqVisitor<ILinqVisitorState>> FieldPathLinqVisitorProvider,
    Func<ISchemaConfig, IFieldFunctionLinqVisitor<ILinqVisitorState>> FieldFunctionLinqVisitorProvider,
    Func<ISchemaConfig, IFieldArgumentLinqVisitor<ILinqVisitorState>> FieldArgumentLinqVisitorProvider,
    Func<ISchemaConfig, IConstantLinqVisitor<ILinqVisitorState>> ConstantLinqVisitorProvider,
    Func<ISchemaConfig, IFilterLinqVisitor<ILinqVisitorState>> FilterLinqVisitorProvider,
    Func<ISchemaConfig, ISortLinqVisitor<ISortLinqVisitorState>> SortLinqVisitorProvider,
    Func<ISchemaConfig, IRequestConfig, ILinqVisitorState> LinqVisitorStateFactory,
    Func<ISchemaConfig, IRequestConfig, ISortLinqVisitorState> SortLinqVisitorStateFactory,
    IImmutableList<IFieldFunctionLinqTranslator> FieldFunctionLinqTranslators,
    IImmutableList<IBinaryExpressionLinqTranslator> BinaryExpressionLinqTranslators
) : ImmutableConfigExtension(Name), ILinqConfigExtension
{
    /// <inheritdoc/>
    public IEnumerable<IFieldFunctionLinqTranslator> GetFieldFunctionLinqTranslators() =>
        FieldFunctionLinqTranslators;

    /// <inheritdoc/>
    public IEnumerable<IBinaryExpressionLinqTranslator> GetBinaryExpressionLinqTranslators() =>
        BinaryExpressionLinqTranslators;
}