// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using Castle.Core;
using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using Moq;
using Xunit;

namespace FunQL.Core.Tests.Constants.Visitors;

public class ConstantVisitorTests
{
    [Fact]
    public async Task Visit_ShouldCallVisitorStateOnEnterAndOnExit()
    {
        /* Arrange */
        var node = new Constant(123);
        var cancellationToken = CancellationToken.None;
        var visitor = new ConstantVisitor<IVisitorState>();

        // Mock to test methods are called in order
        var stateMock = new Mock<IVisitorState>(MockBehavior.Strict);
        var stateMockSequence = new MockSequence();
        stateMock.InSequence(stateMockSequence)
            .Setup(it => it.OnEnter(It.IsAny<QueryNode>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        stateMock.InSequence(stateMockSequence)
            .Setup(it => it.OnExit(It.IsAny<QueryNode>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        /* Act */
        await visitor.Visit(node, stateMock.Object, cancellationToken);

        /* Assert */
        stateMock.Verify(it => it.OnEnter(
            It.Is(node, ReferenceEqualityComparer<Constant>.Instance),
            It.Is(cancellationToken, EqualityComparer<CancellationToken>.Default)
        ), Times.Once);
        stateMock.Verify(it => it.OnExit(
            It.Is(node, ReferenceEqualityComparer<Constant>.Instance),
            It.Is(cancellationToken, EqualityComparer<CancellationToken>.Default)
        ), Times.Once);
    }
}