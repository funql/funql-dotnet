// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using System.Reflection;

namespace FunQL.Core.Configs.Extensions;

/// <summary>Extensions related to <see cref="Expression"/>.</summary>
public static class ExpressionExtensions
{
    /// <summary>Gets the <see cref="MemberInfo"/> for given <paramref name="expression"/>.</summary>
    /// <param name="expression">The lambda expression.</param>
    /// <returns>The <see cref="MemberInfo"/>.</returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="expression"/> does not resolve to <see cref="MemberInfo"/>.
    /// </exception>
    public static MemberInfo GetMemberInfo(this LambdaExpression expression)
    {
        var currentExpression = expression.Body;
        while (true)
        {
            switch (currentExpression)
            {
                case MemberExpression memberExpression:
                    return memberExpression.Member;
                // Unwrap UnaryExpression 
                case UnaryExpression unaryExpression:
                    currentExpression = unaryExpression.Operand;
                    break;
                default:
                    throw new ArgumentException(
                        $"Expression '{expression}' must resolve to a member",
                        nameof(expression)
                    );
            }
        }
    }
}