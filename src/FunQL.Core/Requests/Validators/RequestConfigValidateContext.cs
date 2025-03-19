// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Requests.Validators;

/// <summary>
/// Context for validators so they can resolve the configuration from the <paramref name="RequestConfig"/> that may be
/// necessary for validating nodes.
/// </summary>
/// <param name="RequestConfig">Request configuration to use for validating.</param>
public sealed record RequestConfigValidateContext(IRequestConfig RequestConfig) : IValidateContext;