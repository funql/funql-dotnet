// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Counting.Parsers;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Filtering.Parsers;
using FunQL.Core.Inputting.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Limiting.Parsers;
using FunQL.Core.Requests.Parsers;
using FunQL.Core.Skipping.Parsers;
using FunQL.Core.Sorting.Parsers;

namespace FunQL.Core.Schemas.Configs.Parse.Interfaces;

/// <summary>Extension specifying the configuration for parsing requests.</summary>
public interface IParseConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.ParseConfigExtension";

    /// <summary>Provider for the <see cref="IFieldPathParser"/>.</summary>
    public Func<ISchemaConfig, IFieldPathParser> FieldPathParserProvider { get; }

    /// <summary>Provider for the <see cref="IFieldFunctionParser"/>.</summary>
    public Func<ISchemaConfig, IFieldFunctionParser> FieldFunctionParserProvider { get; }

    /// <summary>Provider for the <see cref="IFieldArgumentParser"/>.</summary>
    public Func<ISchemaConfig, IFieldArgumentParser> FieldArgumentParserProvider { get; }

    /// <summary>Provider for the <see cref="IConstantParser"/>.</summary>
    public Func<ISchemaConfig, IConstantParser> ConstantParserProvider { get; }

    /// <summary>Provider for the <see cref="IInputConstantParser"/>.</summary>
    public Func<ISchemaConfig, IInputConstantParser> InputConstantParserProvider { get; }

    /// <summary>Provider for the <see cref="IInputParser"/>.</summary>
    public Func<ISchemaConfig, IInputParser> InputParserProvider { get; }

    /// <summary>Provider for the <see cref="IFilterConstantParser"/>.</summary>
    public Func<ISchemaConfig, IFilterConstantParser> FilterConstantParserProvider { get; }

    /// <summary>Provider for the <see cref="IFilterParser"/>.</summary>
    public Func<ISchemaConfig, IFilterParser> FilterParserProvider { get; }

    /// <summary>Provider for the <see cref="ISortParser"/>.</summary>
    public Func<ISchemaConfig, ISortParser> SortParserProvider { get; }

    /// <summary>Provider for the <see cref="ISkipParser"/>.</summary>
    public Func<ISchemaConfig, ISkipParser> SkipParserProvider { get; }

    /// <summary>Provider for the <see cref="ILimitParser"/>.</summary>
    public Func<ISchemaConfig, ILimitParser> LimitParserProvider { get; }

    /// <summary>Provider for the <see cref="ICountParser"/>.</summary>
    public Func<ISchemaConfig, ICountParser> CountParserProvider { get; }

    /// <summary>Provider for the <see cref="IParameterParser"/>.</summary>
    public Func<ISchemaConfig, IParameterParser> ParameterParserProvider { get; }

    /// <summary>Provider for the <see cref="IRequestParser"/>.</summary>
    public Func<ISchemaConfig, IRequestParser> RequestParserProvider { get; }

    /// <summary>Factory to create the <see cref="IParserState"/> for given <see cref="ILexer"/>.</summary>
    public Func<ISchemaConfig, ILexer, IParserState> ParserStateFactory { get; }
}