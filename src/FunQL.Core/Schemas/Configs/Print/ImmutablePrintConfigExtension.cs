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
using FunQL.Core.Schemas.Configs.Print.Interfaces;
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Schemas.Configs.Print;

/// <summary>Immutable implementation of <see cref="IPrintConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="FieldPathPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.FieldPathPrintVisitorProvider"/>.
/// </param>
/// <param name="FieldFunctionPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.FieldFunctionPrintVisitorProvider"/>.
/// </param>
/// <param name="FieldArgumentPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.FieldArgumentPrintVisitorProvider"/>.
/// </param>
/// <param name="ConstantPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.ConstantPrintVisitorProvider"/>.
/// </param>
/// <param name="InputPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.InputPrintVisitorProvider"/>.
/// </param>
/// <param name="FilterPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.FilterPrintVisitorProvider"/>.
/// </param>
/// <param name="SortPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.SortPrintVisitorProvider"/>.
/// </param>
/// <param name="SkipPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.SkipPrintVisitorProvider"/>.
/// </param>
/// <param name="LimitPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.LimitPrintVisitorProvider"/>.
/// </param>
/// <param name="CountPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.CountPrintVisitorProvider"/>.
/// </param>
/// <param name="ParameterPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.ParameterPrintVisitorProvider"/>.
/// </param>
/// <param name="RequestPrintVisitorProvider">
/// The <see cref="IPrintConfigExtension.RequestPrintVisitorProvider"/>.
/// </param>
/// <param name="PrintVisitorStateFactory">
/// The <see cref="IPrintConfigExtension.PrintVisitorStateFactory"/>.
/// </param>
public sealed record ImmutablePrintConfigExtension(
    string Name,
    Func<ISchemaConfig, IFieldPathPrintVisitor<IPrintVisitorState>> FieldPathPrintVisitorProvider,
    Func<ISchemaConfig, IFieldFunctionPrintVisitor<IPrintVisitorState>> FieldFunctionPrintVisitorProvider,
    Func<ISchemaConfig, IFieldArgumentPrintVisitor<IPrintVisitorState>> FieldArgumentPrintVisitorProvider,
    Func<ISchemaConfig, IConstantPrintVisitor<IPrintVisitorState>> ConstantPrintVisitorProvider,
    Func<ISchemaConfig, IInputPrintVisitor<IPrintVisitorState>> InputPrintVisitorProvider,
    Func<ISchemaConfig, IFilterPrintVisitor<IPrintVisitorState>> FilterPrintVisitorProvider,
    Func<ISchemaConfig, ISortPrintVisitor<IPrintVisitorState>> SortPrintVisitorProvider,
    Func<ISchemaConfig, ISkipPrintVisitor<IPrintVisitorState>> SkipPrintVisitorProvider,
    Func<ISchemaConfig, ILimitPrintVisitor<IPrintVisitorState>> LimitPrintVisitorProvider,
    Func<ISchemaConfig, ICountPrintVisitor<IPrintVisitorState>> CountPrintVisitorProvider,
    Func<ISchemaConfig, IParameterPrintVisitor<IPrintVisitorState>> ParameterPrintVisitorProvider,
    Func<ISchemaConfig, IRequestPrintVisitor<IPrintVisitorState>> RequestPrintVisitorProvider,
    Func<ISchemaConfig, IPrintVisitorState> PrintVisitorStateFactory
) : ImmutableConfigExtension(Name), IPrintConfigExtension;