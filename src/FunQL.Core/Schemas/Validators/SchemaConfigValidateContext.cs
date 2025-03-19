// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Validators;

/// <summary>
/// Context for validators so they can resolve the configuration from the <paramref name="SchemaConfig"/> that may be
/// necessary for validating nodes.
/// </summary>
/// <param name="SchemaConfig">Schema configuration to use for validating.</param>
public sealed record SchemaConfigValidateContext(ISchemaConfig SchemaConfig) : IValidateContext;