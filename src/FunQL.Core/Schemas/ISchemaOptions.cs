// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;

namespace FunQL.Core.Schemas;

/// <summary>Options to pass to <see cref="Schema"/>.</summary>
public interface ISchemaOptions
{
    /// <summary>Configurators to call upon initializing.</summary>
    /// <remarks>These are called before <see cref="Schema.OnInitializeSchema"/>.</remarks>
    public IImmutableList<ISchemaConfigurator> OnInitializeConfigurators { get; }

    /// <summary>Configurators to call upon finalizing.</summary>
    /// <remarks>
    /// These are called after <see cref="Schema.OnInitializeSchema"/> and before <see cref="Schema.OnFinalizeSchema"/>.
    /// This can e.g. be used to configure conventions, like adding function support based on field type.
    /// </remarks>
    public IImmutableList<ISchemaConfigurator> OnFinalizeConfigurators { get; }
}