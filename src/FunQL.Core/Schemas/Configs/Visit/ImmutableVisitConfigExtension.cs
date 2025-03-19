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

/// <summary>Immutable implementation of <see cref="IVisitConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="FieldPathVisitorProvider">
/// The <see cref="IVisitConfigExtension.FieldPathVisitorProvider"/>.
/// </param>
/// <param name="FieldFunctionVisitorProvider">
/// The <see cref="IVisitConfigExtension.FieldFunctionVisitorProvider"/>.
/// </param>
/// <param name="FieldArgumentVisitorProvider">
/// The <see cref="IVisitConfigExtension.FieldArgumentVisitorProvider"/>.
/// </param>
/// <param name="ConstantVisitorProvider">
/// The <see cref="IVisitConfigExtension.ConstantVisitorProvider"/>.
/// </param>
/// <param name="InputVisitorProvider">
/// The <see cref="IVisitConfigExtension.InputVisitorProvider"/>.
/// </param>
/// <param name="FilterVisitorProvider">
/// The <see cref="IVisitConfigExtension.FilterVisitorProvider"/>.
/// </param>
/// <param name="SortVisitorProvider">
/// The <see cref="IVisitConfigExtension.SortVisitorProvider"/>.
/// </param>
/// <param name="SkipVisitorProvider">
/// The <see cref="IVisitConfigExtension.SkipVisitorProvider"/>.
/// </param>
/// <param name="LimitVisitorProvider">
/// The <see cref="IVisitConfigExtension.LimitVisitorProvider"/>.
/// </param>
/// <param name="CountVisitorProvider">
/// The <see cref="IVisitConfigExtension.CountVisitorProvider"/>.
/// </param>
/// <param name="ParameterVisitorProvider">
/// The <see cref="IVisitConfigExtension.ParameterVisitorProvider"/>.
/// </param>
/// <param name="RequestVisitorProvider">
/// The <see cref="IVisitConfigExtension.RequestVisitorProvider"/>.
/// </param>
public sealed record ImmutableVisitConfigExtension(
    string Name,
    Func<ISchemaConfig, IFieldPathVisitor<IVisitorState>> FieldPathVisitorProvider,
    Func<ISchemaConfig, IFieldFunctionVisitor<IVisitorState>> FieldFunctionVisitorProvider,
    Func<ISchemaConfig, IFieldArgumentVisitor<IVisitorState>> FieldArgumentVisitorProvider,
    Func<ISchemaConfig, IConstantVisitor<IVisitorState>> ConstantVisitorProvider,
    Func<ISchemaConfig, IInputVisitor<IVisitorState>> InputVisitorProvider,
    Func<ISchemaConfig, IFilterVisitor<IVisitorState>> FilterVisitorProvider,
    Func<ISchemaConfig, ISortVisitor<IVisitorState>> SortVisitorProvider,
    Func<ISchemaConfig, ISkipVisitor<IVisitorState>> SkipVisitorProvider,
    Func<ISchemaConfig, ILimitVisitor<IVisitorState>> LimitVisitorProvider,
    Func<ISchemaConfig, ICountVisitor<IVisitorState>> CountVisitorProvider,
    Func<ISchemaConfig, IParameterVisitor<IVisitorState>> ParameterVisitorProvider,
    Func<ISchemaConfig, IRequestVisitor<IVisitorState>> RequestVisitorProvider
) : ImmutableConfigExtension(Name), IVisitConfigExtension;