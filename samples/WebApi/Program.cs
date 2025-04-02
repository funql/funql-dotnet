// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using WebApi.EFCore;
using WebApi.Endpoints;
using WebApi.Exceptions;
using WebApi.FunQL;
using WebApi.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ===== Services =====
// Add EFCore services
builder.Services.AddDbContext<ApiContext>();
// Add FunQL service
builder.Services.AddSingleton<ApiSchema>();
// Configure JsonSerializerOptions
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ConfigureForWebApi();
});
// Add exception handling services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<FunQLExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Create ApiContext database for this demo: Only using this to seed the in-memory database for this demo; Use e.g.
    // EFCore Migrations in production instead
    new ApiContext().Database.EnsureCreated();
}

app.UseHttpsRedirection();

// Use exception handlers
app.UseExceptionHandler();
app.UseStatusCodePages();

// ===== Endpoints =====
// Map the endpoints for the 'Set' data
app.MapSetEndpoints();

app.Run();