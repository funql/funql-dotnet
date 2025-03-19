// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Static class to define the default translators.</summary>
public static class FieldFunctionLinqTranslators
{
    /// <summary>List with default <see cref="IFieldFunctionLinqTranslator"/>.</summary>
    public static readonly IEnumerable<IFieldFunctionLinqTranslator> DefaultTranslators =
    [
        new ObjectFunctionLinqTranslator(),
        new DateTimeFunctionLinqTranslator(),
        new DoubleFunctionLinqTranslator(),
        new DecimalFunctionLinqTranslator(),
        new StringFunctionLinqTranslator()
    ];
}