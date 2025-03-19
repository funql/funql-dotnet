// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Parsers;

/// <summary>Parse error messages related to <see cref="Constant"/>.</summary>
internal static class ConstantParseErrors
{
    /// <summary>Failed to parse constant '{value}' at position {position}.</summary>
    public static string FailedToParse(string value, int position) =>
        $"Failed to parse constant '{value}' at position {position}.";
}