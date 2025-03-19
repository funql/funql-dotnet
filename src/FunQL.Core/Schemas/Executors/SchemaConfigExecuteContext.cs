// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Executors;

/// <summary>
/// Context for executors so they can resolve the configuration from the <paramref name="SchemaConfig"/> that may be
/// necessary for executing.
/// </summary>
/// <param name="SchemaConfig">Schema configuration to use for executing.</param>
public sealed record SchemaConfigExecuteContext(ISchemaConfig SchemaConfig) : IExecuteContext;