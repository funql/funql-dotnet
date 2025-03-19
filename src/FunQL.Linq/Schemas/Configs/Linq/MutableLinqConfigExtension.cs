// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using System.Linq.Expressions;
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

/// <summary>
/// Default implementation of <see cref="IMutableLinqConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
///   - <see cref="LinqVisitorStateFactory"/> will enter <see cref="FieldPathLinqVisitContext"/>
///   - <see cref="SortLinqVisitorStateFactory"/> will enter <see cref="FieldPathLinqVisitContext"/>
///   - <see cref="GetFieldFunctionLinqTranslators"/> will add
///     <see cref="FieldFunctionLinqTranslators.DefaultTranslators"/> by default
///   - <see cref="GetBinaryExpressionLinqTranslators"/> will add
///     <see cref="BinaryExpressionLinqTranslators.DefaultTranslators"/> by default
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableLinqConfigExtension : MutableConfigExtension, IMutableLinqConfigExtension
{
    /// <summary>Tries to resolve <see cref="IObjectTypeConfig"/> for given <paramref name="typeConfig"/>.</summary>
    private static IObjectTypeConfig ResolveObjectTypeConfig(string requestName, ITypeConfig typeConfig) =>
        typeConfig switch
        {
            IObjectTypeConfig objectTypeConfig => objectTypeConfig,
            IListTypeConfig listTypeConfig => ResolveObjectTypeConfig(requestName, listTypeConfig.ElementTypeConfig),
            _ => throw new InvalidOperationException(
                $"LINQ translation requires request '{requestName}' to return either an Object or a List."
            ),
        };

    /// <summary>Current list of <see cref="IFieldFunctionLinqTranslator"/>.</summary>
    private readonly List<IFieldFunctionLinqTranslator> _fieldFunctionLinqTranslators =
        FieldFunctionLinqTranslators.DefaultTranslators.ToList();

    /// <summary>Current list of <see cref="IBinaryExpressionLinqTranslator"/>.</summary>
    private readonly List<IBinaryExpressionLinqTranslator> _binaryExpressionLinqTranslators =
        BinaryExpressionLinqTranslators.DefaultTranslators.ToList();

    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutableLinqConfigExtension(string name) : base(name)
    {
        IFieldPathLinqVisitor<ILinqVisitorState>? fieldPathLinqVisitor = null;
        FieldPathLinqVisitorProvider = _ => fieldPathLinqVisitor ??= new FieldPathLinqVisitor();

        IFieldFunctionLinqVisitor<ILinqVisitorState>? fieldFunctionLinqVisitor = null;
        FieldFunctionLinqVisitorProvider = schemaConfig => fieldFunctionLinqVisitor ??= new FieldFunctionLinqVisitor(
            _fieldFunctionLinqTranslators,
            FieldPathLinqVisitorProvider(schemaConfig)
        );

        IFieldArgumentLinqVisitor<ILinqVisitorState>? fieldArgumentLinqVisitor = null;
        FieldArgumentLinqVisitorProvider = schemaConfig => fieldArgumentLinqVisitor ??= new FieldArgumentLinqVisitor(
            FieldFunctionLinqVisitorProvider(schemaConfig)
        );

        IConstantLinqVisitor<ILinqVisitorState>? constantLinqVisitor = null;
        ConstantLinqVisitorProvider = _ => constantLinqVisitor ??= new ConstantLinqVisitor();

        IFilterLinqVisitor<ILinqVisitorState>? filterLinqVisitor = null;
        FilterLinqVisitorProvider = schemaConfig => filterLinqVisitor ??= new FilterLinqVisitor(
            _binaryExpressionLinqTranslators,
            FieldArgumentLinqVisitorProvider(schemaConfig),
            ConstantLinqVisitorProvider(schemaConfig)
        );

        ISortLinqVisitor<ISortLinqVisitorState>? sortLinqVisitor = null;
        SortLinqVisitorProvider = schemaConfig => sortLinqVisitor ??= new SortLinqVisitor(
            FieldArgumentLinqVisitorProvider(schemaConfig)
        );

        LinqVisitorStateFactory = (schemaConfig, requestConfig) =>
        {
            var objectTypeConfig = ResolveObjectTypeConfig(requestConfig.Name, requestConfig.ReturnTypeConfig);
            var type = objectTypeConfig.Type;
            var source = Expression.Parameter(type, "it");
            var state = new LinqVisitorState(source, HandleNullPropagation);
            state.EnterContext(new FieldPathLinqVisitContext(objectTypeConfig));
            return state;
        };
        SortLinqVisitorStateFactory = (schemaConfig, requestConfig) =>
        {
            var objectTypeConfig = ResolveObjectTypeConfig(requestConfig.Name, requestConfig.ReturnTypeConfig);
            var type = objectTypeConfig.Type;
            var source = Expression.Parameter(type, "it");
            var state = new SortLinqVisitorState(source, HandleNullPropagation);
            state.EnterContext(new FieldPathLinqVisitContext(objectTypeConfig));
            return state;
        };
    }

    /// <inheritdoc cref="IMutableLinqConfigExtension.HandleNullPropagation"/>
    public bool HandleNullPropagation { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.FieldPathLinqVisitorProvider"/>
    public Func<ISchemaConfig, IFieldPathLinqVisitor<ILinqVisitorState>> FieldPathLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.FieldFunctionLinqVisitorProvider"/>
    public Func<ISchemaConfig, IFieldFunctionLinqVisitor<ILinqVisitorState>> FieldFunctionLinqVisitorProvider
    {
        get;
        set;
    }

    /// <inheritdoc cref="IMutableLinqConfigExtension.FieldArgumentLinqVisitorProvider"/>
    public Func<ISchemaConfig, IFieldArgumentLinqVisitor<ILinqVisitorState>> FieldArgumentLinqVisitorProvider
    {
        get;
        set;
    }

    /// <inheritdoc cref="IMutableLinqConfigExtension.ConstantLinqVisitorProvider"/>
    public Func<ISchemaConfig, IConstantLinqVisitor<ILinqVisitorState>> ConstantLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.FilterLinqVisitorProvider"/>
    public Func<ISchemaConfig, IFilterLinqVisitor<ILinqVisitorState>> FilterLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.SortLinqVisitorProvider"/>
    public Func<ISchemaConfig, ISortLinqVisitor<ISortLinqVisitorState>> SortLinqVisitorProvider { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.LinqVisitorStateFactory"/>
    public Func<ISchemaConfig, IRequestConfig, ILinqVisitorState> LinqVisitorStateFactory { get; set; }

    /// <inheritdoc cref="IMutableLinqConfigExtension.SortLinqVisitorStateFactory"/>
    public Func<ISchemaConfig, IRequestConfig, ISortLinqVisitorState> SortLinqVisitorStateFactory { get; set; }

    /// <inheritdoc/>
    public IEnumerable<IFieldFunctionLinqTranslator> GetFieldFunctionLinqTranslators() => _fieldFunctionLinqTranslators;

    /// <inheritdoc/>
    public void AddFieldFunctionLinqTranslator(IFieldFunctionLinqTranslator translator)
    {
        _fieldFunctionLinqTranslators.Add(translator);
    }

    /// <inheritdoc/>
    public void RemoveFieldFunctionLinqTranslator(IFieldFunctionLinqTranslator translator)
    {
        _fieldFunctionLinqTranslators.Remove(translator);
    }

    /// <inheritdoc/>
    public IEnumerable<IBinaryExpressionLinqTranslator> GetBinaryExpressionLinqTranslators() =>
        _binaryExpressionLinqTranslators;

    /// <inheritdoc/>
    public void AddBinaryExpressionLinqTranslator(IBinaryExpressionLinqTranslator translator)
    {
        _binaryExpressionLinqTranslators.Add(translator);
    }

    /// <inheritdoc/>
    public void RemoveBinaryExpressionLinqTranslator(IBinaryExpressionLinqTranslator translator)
    {
        _binaryExpressionLinqTranslators.Remove(translator);
    }

    /// <inheritdoc cref="IMutableLinqConfigExtension.ToConfig"/>
    public override ILinqConfigExtension ToConfig() => new ImmutableLinqConfigExtension(
        Name, HandleNullPropagation, FieldPathLinqVisitorProvider, FieldFunctionLinqVisitorProvider,
        FieldArgumentLinqVisitorProvider, ConstantLinqVisitorProvider, FilterLinqVisitorProvider,
        SortLinqVisitorProvider, LinqVisitorStateFactory, SortLinqVisitorStateFactory,
        _fieldFunctionLinqTranslators.ToImmutableList(), _binaryExpressionLinqTranslators.ToImmutableList()
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}