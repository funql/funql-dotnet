// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors.Fields;

/// <summary>
/// Context for <see cref="IFieldPathLinqVisitor{ILinqVisitorState}"/> when translating a lambda expression. When this
/// context is available, the given <see cref="LambdaParameter"/> should be used when translating a path meant for a
/// lambda expression.
/// </summary>
/// <param name="LambdaParameter">Parameter to use for the lambda.</param>
/// <remarks>
/// When creating a lambda expression, it is necessary that all lambda <see cref="ParameterExpression"/> are the same
/// instance. With this context, the same instance can be shared with multiple field path translations. This avoids
/// having to walk the <see cref="Expression"/> tree afterward to replace every <see cref="ParameterExpression"/>
/// instance.
/// </remarks>
public sealed record LambdaVisitContext(ParameterExpression LambdaParameter) : IVisitContext;