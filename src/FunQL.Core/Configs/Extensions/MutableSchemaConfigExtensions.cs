// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Extensions;

/// <summary>Extensions related to <see cref="IMutableSchemaConfig"/>.</summary>
public static class MutableSchemaConfigExtensions
{
    /// <summary>
    /// Adds a <see cref="MutableFunctionConfig"/> to the <paramref name="schemaConfig"/>, specifying its return type
    /// and argument types.
    /// </summary>
    /// <param name="schemaConfig">The schema config to which the function is added.</param>
    /// <param name="name">The name of the function.</param>
    /// <param name="returnType">The return type of the function.</param>
    /// <param name="argumentTypes">The argument types of the function.</param>
    public static void AddFunctionConfig(
        this IMutableSchemaConfig schemaConfig,
        string name,
        Type returnType,
        params Type[] argumentTypes
    )
    {
        schemaConfig.AddFunctionConfig(new MutableFunctionConfig(name, new MutableSimpleTypeConfig(returnType))
        {
            ArgumentTypeConfigs = argumentTypes
                .Select(it => new MutableSimpleTypeConfig(it))
                .ToImmutableList()
        });
    }
}