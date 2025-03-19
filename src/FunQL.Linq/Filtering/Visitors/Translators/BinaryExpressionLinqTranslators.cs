// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>Static class to define the default translators.</summary>
public static class BinaryExpressionLinqTranslators
{
    /// <summary>List with default <see cref="IBinaryExpressionLinqTranslator"/>.</summary>
    public static readonly IEnumerable<IBinaryExpressionLinqTranslator> DefaultTranslators =
    [
        new ObjectBinaryExpressionLinqTranslator(),
        new EnumBinaryExpressionLinqTranslator(),
        new StringBinaryExpressionLinqTranslator(),
        new GuidBinaryExpressionLinqTranslator()
    ];
}