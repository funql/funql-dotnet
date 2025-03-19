// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>Represents a group of <see cref="IValidationRule"/> that this rule will delegate validation to.</summary>
/// <param name="rules">Rules to delegate validation to.</param>
public class CompositeValidationRule(params IValidationRule[] rules) : IValidationRule
{
    /// <summary>
    /// Cache of <see cref="IValidationRule"/> grouped by the <see cref="IValidationRule.NodeType"/> they validate. This
    /// cache is filled upon initialization.
    /// </summary>
    /// <remarks>
    /// Used to more efficiently determine which <see cref="IValidationRule"/> apply to a specific
    /// <see cref="QueryNode"/> <see cref="Type"/> and all its <see cref="Type.BaseType"/>.
    /// </remarks>
    private readonly IReadOnlyDictionary<Type, List<IValidationRule>> _rulesByValidationType = BuildRulesCache(rules);

    /// <summary>
    /// Cache of <see cref="IValidationRule"/> grouped by the root (sealed/final) <see cref="Type"/> of a
    /// <see cref="QueryNode"/> for which they should be called. This cache is filled at runtime.
    /// </summary>
    /// <remarks>Used to avoid redundant iterations on each validation call.</remarks>
    private readonly Dictionary<Type, List<IValidationRule>> _rulesByRootType = new();

    /// <inheritdoc/>
    /// <remarks>
    /// Type is <see cref="QueryNode"/> as this rule needs to be called for each node type to allow for delegating
    /// validation.
    /// </remarks>
    public Type NodeType { get; } = typeof(QueryNode);

    /// <inheritdoc/>
    public async Task ValidateOnEnter(QueryNode node, IValidatorState state, CancellationToken cancellationToken)
    {
        var nodeType = node.GetType();

        // Call cached rules if available
        if (_rulesByRootType.TryGetValue(nodeType, out var cachedRules))
        {
            foreach (var rule in cachedRules)
            {
                await rule.ValidateOnEnter(node, state, cancellationToken);
            }
        }
        else
        {
            // No cached rules, so determine rules for given node type and cache them for later
            cachedRules = [];
            _rulesByRootType[nodeType] = cachedRules;

            // Validate the node for its type and all its base types 
            var currentType = nodeType;
            while (currentType != null)
            {
                if (_rulesByValidationType.TryGetValue(currentType, out var rules))
                {
                    foreach (var rule in rules)
                    {
                        await rule.ValidateOnEnter(node, state, cancellationToken);
                    }

                    // Add the rules to the cache
                    cachedRules.AddRange(rules);
                }

                currentType = currentType.BaseType;
            }
        }
    }

    /// <inheritdoc/>
    public async Task ValidateOnExit(QueryNode node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (!_rulesByRootType.TryGetValue(node.GetType(), out var cachedRules))
            throw new InvalidOperationException($"Node '{node}' must be entered before exiting");

        // On exit, we need to reverse callbacks so that a validator entered first is exited last, otherwise calls to
        // .ExitContext() may be called in unexpected order
        foreach (var rule in cachedRules.AsEnumerable().Reverse())
        {
            await rule.ValidateOnExit(node, state, cancellationToken);
        }
    }

    /// <summary>
    /// Builds the rules cache for given <paramref name="rules"/>, caching a list of <see cref="IValidationRule"/> per
    /// <see cref="QueryNode"/> <see cref="Type"/> to avoid iterating the entire rules list on each
    /// <see cref="ValidateOnEnter"/> call.
    /// </summary>
    /// <param name="rules">Rules to build cache for.</param>
    /// <returns>The rules cache.</returns>
    private static Dictionary<Type, List<IValidationRule>> BuildRulesCache(params IValidationRule[] rules)
    {
        var dict = new Dictionary<Type, List<IValidationRule>>();
        foreach (var rule in rules)
        {
            var nodeType = rule.NodeType;
            if (!dict.TryGetValue(nodeType, out var ruleList))
            {
                ruleList = [];
                dict.Add(nodeType, ruleList);
            }

            ruleList.Add(rule);
        }

        return dict;
    }
}