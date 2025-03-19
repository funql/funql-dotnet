// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Tests.TestData.Models;

public sealed record SetMinifigure
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}