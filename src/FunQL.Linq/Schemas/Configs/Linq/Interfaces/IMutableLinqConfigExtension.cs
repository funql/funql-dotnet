// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
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

/// <summary>Mutable version of <see cref="ILinqConfigExtension"/>.</summary>
public interface IMutableLinqConfigExtension : ILinqConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="ILinqConfigExtension.HandleNullPropagation"/>
    public new bool HandleNullPropagation { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.FieldPathLinqVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldPathLinqVisitor<ILinqVisitorState>> FieldPathLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.FieldFunctionLinqVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldFunctionLinqVisitor<ILinqVisitorState>> FieldFunctionLinqVisitorProvider
    {
        get;
        set;
    }

    /// <inheritdoc cref="ILinqConfigExtension.FieldArgumentLinqVisitorProvider"/>
    public new Func<ISchemaConfig, IFieldArgumentLinqVisitor<ILinqVisitorState>> FieldArgumentLinqVisitorProvider
    {
        get;
        set;
    }

    /// <inheritdoc cref="ILinqConfigExtension.ConstantLinqVisitorProvider"/>
    public new Func<ISchemaConfig, IConstantLinqVisitor<ILinqVisitorState>> ConstantLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.FilterLinqVisitorProvider"/>
    public new Func<ISchemaConfig, IFilterLinqVisitor<ILinqVisitorState>> FilterLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.SortLinqVisitorProvider"/>
    public new Func<ISchemaConfig, ISortLinqVisitor<ISortLinqVisitorState>> SortLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.LinqVisitorStateFactory"/>
    public new Func<ISchemaConfig, IRequestConfig, ILinqVisitorState> LinqVisitorStateFactory { get; set; }

    /// <inheritdoc cref="ILinqConfigExtension.SortLinqVisitorStateFactory"/>
    public new Func<ISchemaConfig, IRequestConfig, ISortLinqVisitorState> SortLinqVisitorStateFactory { get; set; }

    /// <summary>Adds given <paramref name="translator"/>.</summary>
    /// <param name="translator">Translator to add.</param>
    public void AddFieldFunctionLinqTranslator(IFieldFunctionLinqTranslator translator);

    /// <summary>Removes given <paramref name="translator"/>.</summary>
    /// <param name="translator">Translator to remove.</param>
    public void RemoveFieldFunctionLinqTranslator(IFieldFunctionLinqTranslator translator);

    /// <summary>Adds given <paramref name="translator"/>.</summary>
    /// <param name="translator">Translator to add.</param>
    public void AddBinaryExpressionLinqTranslator(IBinaryExpressionLinqTranslator translator);

    /// <summary>Removes given <paramref name="translator"/>.</summary>
    /// <param name="translator">Translator to remove.</param>
    public void RemoveBinaryExpressionLinqTranslator(IBinaryExpressionLinqTranslator translator);

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new ILinqConfigExtension ToConfig();
}