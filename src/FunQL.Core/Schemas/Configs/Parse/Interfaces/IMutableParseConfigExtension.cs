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

/// <summary>Mutable version of <see cref="IParseConfigExtension"/>.</summary>
public interface IMutableParseConfigExtension : IParseConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IParseConfigExtension.FieldPathParserProvider"/>
    public new Func<ISchemaConfig, IFieldPathParser> FieldPathParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.FieldFunctionParserProvider"/>
    public new Func<ISchemaConfig, IFieldFunctionParser> FieldFunctionParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.FieldArgumentParserProvider"/>
    public new Func<ISchemaConfig, IFieldArgumentParser> FieldArgumentParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.ConstantParserProvider"/>
    public new Func<ISchemaConfig, IConstantParser> ConstantParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.InputConstantParserProvider"/>
    public new Func<ISchemaConfig, IInputConstantParser> InputConstantParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.InputParserProvider"/>
    public new Func<ISchemaConfig, IInputParser> InputParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.FilterConstantParserProvider"/>
    public new Func<ISchemaConfig, IFilterConstantParser> FilterConstantParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.FilterParserProvider"/>
    public new Func<ISchemaConfig, IFilterParser> FilterParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.SortParserProvider"/>
    public new Func<ISchemaConfig, ISortParser> SortParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.SkipParserProvider"/>
    public new Func<ISchemaConfig, ISkipParser> SkipParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.LimitParserProvider"/>
    public new Func<ISchemaConfig, ILimitParser> LimitParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.CountParserProvider"/>
    public new Func<ISchemaConfig, ICountParser> CountParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.ParameterParserProvider"/>
    public new Func<ISchemaConfig, IParameterParser> ParameterParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.RequestParserProvider"/>
    public new Func<ISchemaConfig, IRequestParser> RequestParserProvider { get; set; }

    /// <inheritdoc cref="IParseConfigExtension.ParserStateFactory"/>
    public new Func<ISchemaConfig, ILexer, IParserState> ParserStateFactory { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IParseConfigExtension ToConfig();
}