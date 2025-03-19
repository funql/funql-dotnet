﻿// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Requests.Parsers;

/// <summary>
/// Context for parsers so they can resolve the configuration from the <paramref name="SchemaConfig"/> that may be
/// necessary to determine how to parse certain nodes, e.g. constants.
/// </summary>
/// <param name="SchemaConfig">Schema configuration to use for parsing a query.</param>
public sealed record SchemaConfigParseContext(ISchemaConfig SchemaConfig) : IParseContext;