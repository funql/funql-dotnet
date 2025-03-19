// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Tests.TestData.Models;

public sealed record Set
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int SetNumber { get; set; }
    public IReadOnlyDictionary<Region, int> ItemNumber { get; set; } = null!;
    public int Pieces { get; set; }
    public double Price { get; set; }
    public Guid DesignerId { get; set; }
    public Designer Designer { get; set; } = null!;
    public DateTime LaunchTime { get; set; }
    public PackagingType PackagingType { get; set; }
    public Theme Theme { get; set; }
    public IReadOnlyList<Category> Categories { get; set; } = null!;
    public IReadOnlyList<Minifigure> Minifigures { get; set; } = null!;
}