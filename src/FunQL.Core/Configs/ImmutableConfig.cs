// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class for an immutable <see cref="IConfig"/>.</summary>
public abstract record ImmutableConfig : IConfig;