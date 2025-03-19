// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Inputting.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IInputConfigExtension"/>.</summary>
public interface IMutableInputConfigExtension : IMutableConfigExtension, IInputConfigExtension
{
    /// <inheritdoc cref="IInputConfigExtension.TypeConfig"/>
    public new IMutableTypeConfig TypeConfig { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IInputConfigExtension ToConfig();
}