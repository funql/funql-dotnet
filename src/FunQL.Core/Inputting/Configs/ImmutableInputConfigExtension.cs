// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Interfaces;

namespace FunQL.Core.Inputting.Configs;

/// <summary>Immutable implementation of <see cref="IInputConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="TypeConfig">Config of the type of the input.</param>
public sealed record ImmutableInputConfigExtension(
    string Name,
    ITypeConfig TypeConfig
) : ImmutableConfigExtension(Name), IInputConfigExtension;