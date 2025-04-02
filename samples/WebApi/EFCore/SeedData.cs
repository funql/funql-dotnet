// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using NodaTime;
using WebApi.Models;

namespace WebApi.EFCore;

/// <summary>Data to seed EFCore database with.</summary>
public static class SeedData
{
    /// <summary>Seed data for the <see cref="Set"/> items.</summary>
    public static readonly IReadOnlyList<Set> Sets =
    [
        new("LEGO Star Wars Millennium Falcon", 75192, 7541, 849.99, Instant.FromUtc(2017, 10, 1, 0, 0)),
        new("LEGO Star Wars The Razor Crest", 75331, 6187, 599.99, Instant.FromUtc(2022, 10, 3, 0, 0)),
        new("LEGO DC Batman Batmobile Tumbler", 76240, 2049, 269.99, Instant.FromUtc(2021, 11, 1, 0, 0)),
        new("LEGO Harry Potter Hogwarts Castle", 71043, 6020, 469.99, Instant.FromUtc(2018, 9, 1, 0, 0)),
        new("LEGO Horizon Forbidden West Tallneck", 76989, 1222, 89.99, Instant.FromUtc(2022, 5, 1, 0, 0)),
        new("LEGO Stranger Things The Upside Down", 75810, 2287, 199.99, Instant.FromUtc(2019, 6, 1, 0, 0)),
        new("LEGO Icons Back to the Future Time Machine", 10300, 1872, 199.99, Instant.FromUtc(2022, 4, 1, 0, 0)),
        new("LEGO Icons McLaren MP4/4 & Ayrton Senna", 10330, 693, 79.99, Instant.FromUtc(2024, 3, 1, 0, 0)),
        new("LEGO Ideas WALL-E", 21303, 677, 49.99, Instant.FromUtc(2015, 9, 1, 0, 0)),
    ];
}