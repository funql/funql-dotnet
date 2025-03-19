// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Executors;

/// <summary>An execution step in a pipeline.</summary>
/// <param name="Name">The name to identify this execution step.</param>
/// <param name="ExecutorProvider">
/// Provider that takes the next <see cref="ExecutorDelegate"/> to be called by the returned
/// <see cref="ExecutorDelegate"/> when next step should run.
/// </param>
/// <param name="Order">Order of this execution step in the pipeline. Lower values indicate earlier execution.</param>
public record ExecutionStep(string Name, Func<ExecutorDelegate, ExecutorDelegate> ExecutorProvider, int Order);