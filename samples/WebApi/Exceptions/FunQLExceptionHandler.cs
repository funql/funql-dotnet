// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Validators.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Exceptions;

/// <summary>Exception handler that writes <see cref="ProblemDetails"/> for <see cref="FunQL"/> exceptions.</summary>
/// <param name="problemDetailsService">Service to write <see cref="ProblemDetails"/>.</param>
public class FunQLExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    /// <summary>Service to write <see cref="ProblemDetails"/>.</summary>
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
    
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken
    )
    {
        var problemDetails = exception switch
        {
            ParseException => new ProblemDetails
            {
                Status = 400,
                Title = "Invalid argument",
                Detail = exception.Message
            },
            ValidationException e => new ProblemDetails
            {
                Status = 422,
                Title = "Invalid argument",
                Detail = e.Message,
                Extensions = new Dictionary<string, object?>
                {
                    ["errors"] = e.Errors.Select(it => new { it.Message })
                }
            },
            _ => null
        };

        // Early return if exception is unknown
        if (problemDetails == null)
            return false;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}