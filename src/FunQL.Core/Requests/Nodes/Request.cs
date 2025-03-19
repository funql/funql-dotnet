// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Requests.Nodes;

/// <summary>
/// The 'request' node is the root node for a FunQL query. It specifies the <paramref name="Name"/> of the request and
/// the <paramref name="Parameters"/> to use for executing the request.
/// </summary>
/// <param name="Name">Name of the request.</param>
/// <param name="Parameters">Parameters to use for executing the request.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Request(
    string Name,
    IReadOnlyList<Parameter> Parameters,
    Metadata? Metadata = null
) : QueryNode(Metadata), IFunctionNode;