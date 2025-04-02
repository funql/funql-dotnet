// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.EFCore;

/// <summary>EFCore <see cref="DbContext"/> for the <see cref="Sets"/> data.</summary>
/// <remarks>Configures an in-memory database for the <see cref="SeedData.Sets"/>.</remarks>
public class ApiContext : DbContext
{
    /// <summary>The <see cref="DbSet{TEntity}"/> to query <see cref="Set"/> data.</summary>
    public DbSet<Set> Sets { get; set; }
    
    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("demo");
        options.UseSeeding((context, _) =>
        {
            context.Set<Set>().AddRange(SeedData.Sets);
            context.SaveChanges();
        });
    }
}