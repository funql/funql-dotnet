// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Tests.TestData.Models;

public sealed record Minifigure
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}