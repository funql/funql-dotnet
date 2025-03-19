// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Extensions;

/// <summary>Extensions related to <see cref="FieldPath"/>.</summary>
public static class FieldPathExtensions
{
    /// <summary>
    /// Unpacks nested fields within <see cref="ListItemField"/> (which represent the path to the parent list field that
    /// the field is an item of), returning each individual field.
    ///
    /// For example a field path like <c>$it.item</c> will return fields <c>list, $it, item</c>.
    /// </summary>
    /// <param name="fieldPath">The field path to unpack.</param>
    /// <returns>Unpacked fields.</returns>
    public static IEnumerable<Field> Unpack(this FieldPath fieldPath)
    {
        foreach (var field in fieldPath.Fields)
        {
            switch (field)
            {
                case ListItemField listItemField:
                    // Unpack the 'parents' of current list item field
                    foreach (var listPathField in listItemField.ListField.Unpack())
                    {
                        yield return listPathField;
                    }

                    // Return the list item itself
                    yield return listItemField;
                    break;
                case NamedField:
                    yield return field;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(field));
            }
        }
    }

    /// <summary>
    /// Resolves the <see cref="IFieldConfig"/> and <see cref="ITypeConfig"/> for each field in given
    /// <paramref name="fieldPath"/> based on the provided <paramref name="rootConfig"/>.
    ///
    /// If both configs are <c>null</c>, that means either the <paramref name="fieldPath"/> is invalid for the config,
    /// or the config is configured incorrectly.
    /// 
    /// If <see cref="IFieldConfig"/> is <c>null</c> and <see cref="ITypeConfig"/> is not, the <see cref="Field"/> is a
    /// list item, which has no corresponding <see cref="IFieldConfig"/>. 
    /// </summary>
    /// <param name="fieldPath">The field path to resolve configurations for.</param>
    /// <param name="rootConfig">The root configuration used as a starting point for resolving.</param>
    /// <returns>
    /// The corresponding <see cref="IFieldConfig"/> and <see cref="ITypeConfig"/> for each <see cref="Field"/>, where
    /// configs may be null if not found in <paramref name="rootConfig"/>.
    /// </returns>
    public static IEnumerable<(Field, IFieldConfig?, ITypeConfig?)> ResolveConfigs(
        this FieldPath fieldPath,
        ITypeConfig rootConfig
    )
    {
        // FieldPath for root config implicitly refers to list items in case of a list return type, so get
        // ElementTypeConfig if that's the case
        if (rootConfig is IListTypeConfig rootListTypeConfig)
            rootConfig = rootListTypeConfig.ElementTypeConfig;

        var typeConfig = rootConfig;
        foreach (var field in fieldPath.Unpack())
        {
            IFieldConfig? fieldConfig = null;
            switch (typeConfig)
            {
                case IListTypeConfig listTypeConfig:
                    // Field must be ListItemField: Either configured incorrectly or invalid FieldPath
                    if (field is not ListItemField)
                    {
                        fieldConfig = null;
                        typeConfig = null;
                        break;
                    }

                    // Field represents the list item, so there is no corresponding FieldConfig 
                    fieldConfig = null;
                    typeConfig = listTypeConfig.ElementTypeConfig;
                    break;
                case IObjectTypeConfig objectTypeConfig:
                    switch (field)
                    {
                        // If field is ListItemField, type must be IListTypeConfig: Either configured incorrectly or
                        // invalid FieldPath
                        case ListItemField:
                            fieldConfig = null;
                            typeConfig = null;
                            break;
                        case NamedField namedField:
                            // Note that fieldConfig may be null if config not found
                            fieldConfig = objectTypeConfig.FindFieldConfig(namedField.Name);
                            typeConfig = fieldConfig?.TypeConfig;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(field));
                    }

                    break;
                case ISimpleTypeConfig:
                    // Trying to access a nested field for a field configured as simple field: Either configured
                    // incorrectly or invalid FieldPath
                    fieldConfig = null;
                    typeConfig = null;
                    break;
                case null:
                    // If there's no typeConfig, we can't resolve config for current field, so continue
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeConfig));
            }

            yield return (field, fieldConfig, typeConfig);
        }
    }

    /// <summary>
    /// Converts the <paramref name="fieldPath"/> to a string of the fully unpacked path. For example, <c>$it</c>
    /// referencing <c>items</c> will return <c>items.$it</c>.
    /// </summary>
    /// <param name="fieldPath">Path to convert.</param>
    /// <returns>Unpacked path string.</returns>
    public static string ToUnpackedPathString(this FieldPath fieldPath)
    {
        var stringBuilder = new StringBuilder();
        foreach (var field in fieldPath.Unpack())
        {
            if (stringBuilder.Length > 0)
                stringBuilder.Append('.');
            stringBuilder.Append(field switch
            {
                ListItemField => "$it",
                NamedField namedField => namedField.RequiresBracketNotation()
                    ? $"[\"{namedField.Name.Replace("\"", "\\\"")}\"]"
                    : namedField.Name,
                _ => throw new ArgumentOutOfRangeException(nameof(field))
            });
        }

        return stringBuilder.ToString();
    }
}