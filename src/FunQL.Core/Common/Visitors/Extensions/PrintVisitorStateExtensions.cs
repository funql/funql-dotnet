// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Visitors.Extensions;

/// <summary>Extensions for <see cref="IPrintVisitorState"/>.</summary>
public static class PrintVisitorStateExtensions
{
    /// <summary>Writes given <paramref name="value"/> using <paramref name="state"/>.</summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="value">Value to write.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that writes the value.</returns>
    public static Task Write(this IPrintVisitorState state, string value, CancellationToken cancellationToken) =>
        // No cancellation support for strings
        state.TextWriter.WriteAsync(value);

    /// <summary>
    /// Writes <paramref name="node"/> as a function, writing optional arguments for each printer from
    /// <paramref name="argumentPrinters"/>, using <paramref name="state"/>.
    /// </summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="node">Node to write.</param>
    /// <param name="argumentPrinters">Printers that each write an argument of the function.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that writes the function.</returns>
    public static async Task WriteFunction(
        this IPrintVisitorState state,
        IFunctionNode node,
        IEnumerable<Func<CancellationToken, Task>> argumentPrinters,
        CancellationToken cancellationToken
    )
    {
        await state.Write(node.Name, cancellationToken);
        await state.Write("(", cancellationToken);

        // Write arguments
        var initialItem = true;
        foreach (var printer in argumentPrinters)
        {
            // Write separator if not first item
            if (!initialItem)
                await state.Write(",", cancellationToken);

            // Write argument
            await printer(cancellationToken);

            initialItem = false;
        }

        await state.Write(")", cancellationToken);
    }

    /// <summary>
    /// Visits <paramref name="node"/> and writes it as a function, writing optional arguments for each printer from
    /// <paramref name="argumentPrinters"/>, using <paramref name="state"/>.
    /// </summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="node">Node to write.</param>
    /// <param name="argumentPrinters">Printers that each write an argument of the function.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that visits and writes the function.</returns>
    public static Task VisitAndWriteFunction<TNode>(
        this IPrintVisitorState state,
        TNode node,
        IEnumerable<Func<CancellationToken, Task>> argumentPrinters,
        CancellationToken cancellationToken
    ) where TNode : QueryNode, IFunctionNode => state.OnVisit(
        node,
        ct => state.WriteFunction(node, argumentPrinters, ct),
        cancellationToken
    );

    /// <summary>
    /// Visits <paramref name="node"/> and writes it as a function using <paramref name="state"/>.
    /// </summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="node">Node to write.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that visits and writes the function.</returns>
    public static Task VisitAndWriteFunction<TNode>(
        this IPrintVisitorState state,
        TNode node,
        CancellationToken cancellationToken
    ) where TNode : QueryNode, IFunctionNode => state.OnVisit(
        node,
        ct => state.WriteFunction(node, [], ct),
        cancellationToken
    );

    /// <summary>
    /// Visits <paramref name="node"/> and writes it as a function, writing arguments for each printer, using
    /// <paramref name="state"/>.
    /// </summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="node">Node to write.</param>
    /// <param name="argumentPrinterOne">Printer that writes first argument.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that visits and writes the function.</returns>
    public static Task VisitAndWriteFunction<TNode>(
        this IPrintVisitorState state,
        TNode node,
        Func<CancellationToken, Task> argumentPrinterOne,
        CancellationToken cancellationToken
    ) where TNode : QueryNode, IFunctionNode => state.OnVisit(
        node,
        ct => state.WriteFunction(node, [argumentPrinterOne], ct),
        cancellationToken
    );

    /// <summary>
    /// Visits <paramref name="node"/> and writes it as a function, writing arguments for each printer, using
    /// <paramref name="state"/>.
    /// </summary>
    /// <param name="state">State to use for writing.</param>
    /// <param name="node">Node to write.</param>
    /// <param name="argumentPrinterOne">Printer that writes first argument.</param>
    /// <param name="argumentPrinterTwo">Printer that writes second argument.</param>
    /// <param name="cancellationToken">Token to cancel write.</param>
    /// <returns>Task that visits and writes the function.</returns>
    public static Task VisitAndWriteFunction<TNode>(
        this IPrintVisitorState state,
        TNode node,
        Func<CancellationToken, Task> argumentPrinterOne,
        Func<CancellationToken, Task> argumentPrinterTwo,
        CancellationToken cancellationToken
    ) where TNode : QueryNode, IFunctionNode => state.OnVisit(
        node,
        ct => state.WriteFunction(node, [argumentPrinterOne, argumentPrinterTwo], ct),
        cancellationToken
    );
}