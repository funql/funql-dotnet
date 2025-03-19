// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Validators;

/// <summary>State of a validator.</summary>
public interface IValidatorState : IProcessorState<IValidateContext>
{
    /// <summary>Whether there are any validation errors.</summary>
    public bool HasErrors { get; }

    /// <summary>Current validation errors.</summary>
    /// <seealso cref="AddError"/>
    public IEnumerable<ValidationError> Errors { get; }

    /// <summary>Adds the <paramref name="error"/> to the list of errors.</summary>
    /// <param name="error">Error to add.</param>
    public void AddError(ValidationError error);

    /// <summary>Callback to validate given <paramref name="node"/> upon entering.</summary>
    /// <param name="node">Node to validate.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task to await validation.</returns>
    public Task ValidateOnEnter(QueryNode node, CancellationToken cancellationToken);

    /// <summary>Callback to validate given <paramref name="node"/> upon exiting.</summary>
    /// <param name="node">Node to validate.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task to await validation.</returns>
    public Task ValidateOnExit(QueryNode node, CancellationToken cancellationToken);
}