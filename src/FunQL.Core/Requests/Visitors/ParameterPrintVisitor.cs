// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Counting.Visitors;
using FunQL.Core.Filtering.Visitors;
using FunQL.Core.Inputting.Visitors;
using FunQL.Core.Limiting.Visitors;
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Default implementation of <see cref="IParameterPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="ParameterVisitor{TState}"/>
public class ParameterPrintVisitor<TState>(
    IInputVisitor<TState> inputVisitor,
    IFilterVisitor<TState> filterVisitor,
    ISortVisitor<TState> sortVisitor,
    ISkipVisitor<TState> skipVisitor,
    ILimitVisitor<TState> limitVisitor,
    ICountVisitor<TState> countVisitor
) : ParameterVisitor<TState>(inputVisitor, filterVisitor, sortVisitor, skipVisitor, limitVisitor, countVisitor),
    IParameterPrintVisitor<TState> where TState : IPrintVisitorState;