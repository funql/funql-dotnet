// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>
/// Interface for a translator that translates a <see cref="FieldFunction"/> to a LINQ <see cref="Expression"/>. This
/// allows us to define the translations per <see cref="Type"/> so it can easily be extended for different types. For
/// example, <see cref="DateTime"/> functions may be applied to <c>Instant</c> when using something like NodaTime, so
/// the correct LINQ translations must be supplied to FunQL.
/// </summary>
public interface IFieldFunctionLinqTranslator
{
    /// <summary>The execution order of the translator. Lower values execute first.</summary>
    /// <remarks>This is used to make sure specific translators will always run before others.</remarks>
    public int Order { get; }

    /// <summary>
    /// Tries to translate <paramref name="node"/> to a LINQ <see cref="Expression"/>. Returns <c>null</c> if it can't
    /// translate given <paramref name="source"/>.
    /// </summary>
    /// <param name="node">Node to translate.</param>
    /// <param name="source">Source to translate.</param>
    /// <param name="state">Current state.</param>
    /// <returns>Translated expression.</returns>
    public Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state);
}