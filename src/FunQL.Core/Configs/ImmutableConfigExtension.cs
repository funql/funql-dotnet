// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class for an immutable <see cref="IConfigExtension"/>.</summary>
/// <param name="Name">Name of this extension.</param>
public abstract record ImmutableConfigExtension(string Name) : ImmutableConfig, IConfigExtension;