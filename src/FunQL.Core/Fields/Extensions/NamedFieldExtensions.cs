// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Extensions;

/// <summary>Extensions related to <see cref="NamedField"/>.</summary>
public static class NamedFieldExtensions
{
    /// <summary>Represents the underscore character ('_').</summary>
    private const char Underscore = '_';

    /// <summary>Determines whether the field name requires bracket notation instead of dot notation.</summary>
    /// <param name="field">The <see cref="NamedField"/> to check.</param>
    /// <returns><c>true</c> if the field name requires bracket notation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the field name is empty.</exception>
    /// <remarks>
    /// A field requires bracket notation if it does not conform to dot notation rules. A valid dot notation field must
    /// start with a letter (a-z, A-Z) or an underscore (_), and can only contain letters, digits (0-9), or underscores
    /// (_) thereafter.
    /// </remarks>
    public static bool RequiresBracketNotation(this NamedField field)
    {
        var name = field.Name;
        if (string.IsNullOrEmpty(name))
            throw new InvalidOperationException("Field name must not be empty");

        if (!IsValidFirstCharacter(name[0]))
            return true;

        for (var i = 1; i < name.Length; i++)
        {
            if (!IsValidSubsequentCharacter(name[i]))
                return true;
        }

        return false;
    }

    /// <summary>Checks if the first character of a field name is valid for dot notation.</summary>
    private static bool IsValidFirstCharacter(char character) =>
        char.IsLetter(character) || character == Underscore;

    /// <summary>Checks if a subsequent character in a field name is valid for dot notation.</summary>
    private static bool IsValidSubsequentCharacter(char character) =>
        char.IsLetterOrDigit(character) || character == Underscore;
}