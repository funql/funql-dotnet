// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>Object translator, translating all types (any object) to a binary <see cref="Expression"/>.</summary>
/// <remarks>This is meant as a 'catch all' translator, which should be called after any other translators.</remarks>
public sealed class ObjectBinaryExpressionLinqTranslator : BinaryExpressionLinqTranslator
{
    /// <inheritdoc/>
    /// <remarks>Uses <see cref="int.MaxValue"/> to assure translator is called after any other translators.</remarks>
    public override int Order => int.MaxValue;
}