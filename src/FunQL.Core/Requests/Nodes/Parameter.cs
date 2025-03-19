﻿// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Requests.Nodes;

/// <summary>Base class for parameters used in a FunQL <see cref="Request"/>.</summary>
/// <param name="Name">Name of the parameter.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public abstract record Parameter(
    string Name,
    Metadata? Metadata
) : QueryNode(Metadata), IFunctionNode;