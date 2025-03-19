// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Base class for a field function translator.</summary>
public abstract class FieldFunctionLinqTranslator : IFieldFunctionLinqTranslator
{
    /// <inheritdoc/>
    public virtual int Order => 0;

    /// <inheritdoc/>
    public abstract Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state);
}