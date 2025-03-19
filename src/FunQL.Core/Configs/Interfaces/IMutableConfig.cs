// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IConfig"/>.</summary>
public interface IMutableConfig : IConfig
{
    /// <summary>
    /// Returns a new non-mutable (read-only) <see cref="IConfig"/> representation of this <see cref="IMutableConfig"/>.
    /// </summary>
    /// <returns>New non-mutable <see cref="IConfig"/>.</returns>
    /// <remarks>Ideally the returned <see cref="IConfig"/> is immutable so it can't accidentally be changed.</remarks>
    public IConfig ToConfig();
}