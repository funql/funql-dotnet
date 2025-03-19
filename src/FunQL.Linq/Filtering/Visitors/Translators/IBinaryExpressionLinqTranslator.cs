// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>
/// Interface for a translator that translates a binary <see cref="BooleanExpression"/> to a LINQ
/// <see cref="Expression"/>.
///
/// This allows for customizing the translation logic for specific types. E.g. when comparing <see cref="string"/>, the
/// <see cref="string.Compare(string, string)"/> should be used instead of using <c>stringA &lt; stringB</c>, so it
/// requires custom logic.
/// </summary>
public interface IBinaryExpressionLinqTranslator
{
    /// <summary>The execution order of the translator. Lower values execute first.</summary>
    /// <remarks>This is used to make sure specific translators will always run before others.</remarks>
    public int Order { get; }

    /// <summary>
    /// Translates given <paramref name="node"/> into a LINQ <see cref="Expression"/> or returns <c>null</c> if the
    /// translator can't handle the translation.
    /// </summary>
    /// <param name="node">The <see cref="BooleanExpression"/> to translate.</param>
    /// <param name="left">The left-hand operand of the expression.</param>
    /// <param name="right">The right-hand operand of the expression.</param>
    /// <param name="state">State of the visitor.</param>
    /// <returns>
    /// A translated LINQ <see cref="Expression"/> representing the <see cref="BooleanExpression"/>, or <c>null</c> if
    /// the translator can't handle the translation.
    /// </returns>
    public Expression? Translate(BooleanExpression node, Expression left, Expression right, ILinqVisitorState state);
}