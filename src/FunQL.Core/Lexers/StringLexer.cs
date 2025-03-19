// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Diagnostics;
using FunQL.Core.Common.Parsers.Exceptions;

namespace FunQL.Core.Lexers;

/// <summary>Implementation of <see cref="ILexer"/> for tokenizing a <see cref="string"/>.</summary>
public class StringLexer : ILexer
{
    /// <summary>Text to tokenize.</summary>
    private readonly string _text;

    /// <summary>Length of text.</summary>
    private readonly int _textLen;

    /// <summary>Current position.</summary>
    private int _textPos;

    /// <summary>Current character.</summary>
    private char? _ch;

    /// <summary>The current token.</summary>
    private Token _currentToken = new(TokenType.None, "", 0);

    /// <summary>The next token so we can cache it in case <see cref="PeekNextToken"/> is called.</summary>
    private Token? _nextToken;

    /// <summary>Whether current character is a valid whitespace character.</summary>
    private bool IsValidWhiteSpace => _ch != null && char.IsWhiteSpace(_ch.Value);

    /// <summary>Whether current character is a valid digit character.</summary>
    private bool IsValidDigit => _ch != null && char.IsDigit(_ch.Value);

    /// <summary>
    /// Whether current character is a valid starting character for an <see cref="TokenType.Identifier"/>.
    /// </summary>
    private bool IsValidStartingCharForIdentifier => _ch != null && char.IsLetter(_ch.Value);

    /// <summary>
    /// Whether current character is a valid non-starting character for an <see cref="TokenType.Identifier"/>.
    /// </summary>
    private bool IsValidNonStartingCharForIdentifier => _ch != null && char.IsLetterOrDigit(_ch.Value);

    /// <inheritdoc/>
    public Token CurrentToken => _currentToken;

    /// <summary>Initializes properties.</summary>
    /// <param name="text">The text to tokenize.</param>
    public StringLexer(string text)
    {
        _text = text;
        _textLen = text.Length;
        SetTextPos(0);
        NextToken();
    }

    /// <summary>Sets current text position to <paramref name="textPos"/> and updates lexer state.</summary>
    /// <param name="textPos">Text position to set.</param>
    private void SetTextPos(int textPos)
    {
        _textPos = textPos;
        _ch = _textPos < _textLen ? _text[_textPos] : null;
    }

    /// <summary>Advances to the next character.</summary>
    private void NextChar()
    {
        if (_textPos < _textLen)
        {
            SetTextPos(_textPos + 1);
        }
    }

    /// <inheritdoc/>
    public Token PeekNextToken()
    {
        // Return cached token if available
        if (_nextToken != null)
            return _nextToken;

        // Save state
        var savedTextPos = _textPos;
        var savedCh = _ch;
        var savedCurrentToken = _currentToken;

        // Get next token and cache it
        _nextToken = NextToken();

        // Restore state
        _textPos = savedTextPos;
        _ch = savedCh;
        _currentToken = savedCurrentToken;

        return _nextToken;
    }

    /// <inheritdoc/>
    public Token NextToken()
    {
        // Skip whitespace characters
        ParseWhitespace();

        TokenType tokenType;
        var tokenPos = _textPos;
        switch (_ch)
        {
            case '(':
                NextChar();
                tokenType = TokenType.OpenParen;
                break;
            case ')':
                NextChar();
                tokenType = TokenType.CloseParen;
                break;
            case ',':
                NextChar();
                tokenType = TokenType.Comma;
                break;
            case '-':
                var hasNext = _textPos + 1 < _textLen;
                if (!hasNext || !char.IsDigit(_text[_textPos + 1]))
                {
                    var found = hasNext ? _text[_textPos + 1].ToString() : "";
                    throw SyntaxException(SyntaxErrors.DigitExpected(_textPos, found));
                }

                ParseNumber();
                tokenType = TokenType.Number;
                break;
            case '"':
                ParseString();
                tokenType = TokenType.String;
                break;
            case '{':
                ParseBalancedExpression('{', '}');
                tokenType = TokenType.Object;
                break;
            case '[':
                NextChar();
                tokenType = TokenType.OpenBracket;
                break;
            case ']':
                NextChar();
                tokenType = TokenType.CloseBracket;
                break;
            case '$':
                NextChar();
                tokenType = TokenType.Dollar;
                break;
            case '.':
                NextChar();
                tokenType = TokenType.Dot;
                break;
            default:
                if (IsValidStartingCharForIdentifier)
                {
                    ParseIdentifier();
                    tokenType = TokenType.Identifier;
                    break;
                }

                if (IsValidDigit)
                {
                    ParseNumber();
                    tokenType = TokenType.Number;
                    break;
                }

                if (_textPos == _textLen)
                {
                    tokenType = TokenType.Eof;
                    break;
                }

                throw SyntaxException(SyntaxErrors.InvalidCharacter(_ch?.ToString() ?? "", _textPos));
        }

        var text = _text.Substring(tokenPos, _textPos - tokenPos);
        tokenType = ResolveTokenType(tokenType, text);

        _currentToken = new Token(tokenType, text, tokenPos);
        // Reset cached next token
        _nextToken = null;
        return _currentToken;
    }

    /// <inheritdoc/>
    public Token CurrentTokenAsArray()
    {
        // Early return if current token is already an Array
        if (_currentToken.Type == TokenType.Array)
            return _currentToken;

        Debug.Assert(_currentToken.Type == TokenType.OpenBracket, "Can only convert OpenBracket to Array");

        // Go back 1 character to start parsing at start of Array
        SetTextPos(_textPos - 1);
        var tokenPos = _textPos;
        ParseBalancedExpression('[', ']');

        var text = _text.Substring(tokenPos, _textPos - tokenPos);
        _currentToken = new Token(TokenType.Array, text, tokenPos);
        // Reset cached next token
        _nextToken = null;
        return _currentToken;
    }

    /// <summary>Parses any whitespaces in input stream.</summary>
    private void ParseWhitespace()
    {
        while (IsValidWhiteSpace)
        {
            NextChar();
        }
    }

    /// <summary>Parses next characters as a number.</summary>
    private void ParseNumber()
    {
        Debug.Assert(IsValidDigit || _ch == '-', "IsValidDigit || _ch == '-'");

        // Skip initial digit or minus
        NextChar();
        // Skip over all digits
        while (IsValidDigit)
        {
            NextChar();
        }

        // Early return if not a double
        if (_ch != '.')
            return;

        // Handle doubles
        NextChar();

        if (!IsValidDigit)
            throw SyntaxException(SyntaxErrors.DigitExpected(_textPos, _ch?.ToString() ?? ""));

        while (IsValidDigit)
        {
            NextChar();
        }
    }

    /// <summary>
    /// Parses next characters as a balanced expression: All characters within given <paramref name="startCharacter"/>
    /// and <paramref name="endCharacter"/>, skipping over nested expressions with same start and end character.
    /// </summary>
    /// <param name="startCharacter">Start character of the balanced expression.</param>
    /// <param name="endCharacter">End character of the balanced expression.</param>
    /// <exception cref="Exception">Thrown if the expression is not properly closed.</exception>
    private void ParseBalancedExpression(char startCharacter, char endCharacter)
    {
        Debug.Assert(_ch == startCharacter, "_ch == startCharacter");

        var startPos = _textPos;

        // Skip start character
        NextChar();
        var currentDepth = 1;
        while (currentDepth > 0)
        {
            // Skip strings as they might contain the startCharacter or endCharacter
            if (_ch == '"')
            {
                ParseString();
            }

            if (_ch == startCharacter)
            {
                currentDepth++;
            }
            else if (_ch == endCharacter)
            {
                currentDepth--;
            }

            if (_ch == null)
            {
                var value = _text.Substring(startPos, _textPos - startPos);
                throw SyntaxException(SyntaxErrors.UnbalancedExpression(value, startPos));
            }

            NextChar();
        }
    }

    /// <summary>Parses next characters as a string.</summary>
    private void ParseString()
    {
        Debug.Assert(_ch == '"', "_ch == '\"'");

        var startPos = _textPos;

        // Skip start character
        NextChar();
        var parsingString = true;
        while (parsingString)
        {
            // Closed if character is end character and not escaped
            if (_ch == '"' && _textPos != 0 && _text[_textPos - 1] != '\\')
            {
                parsingString = !parsingString;
            }

            if (_ch == null)
            {
                var value = _text.Substring(startPos, _textPos - startPos);
                throw SyntaxException(SyntaxErrors.UnbalancedString(value, startPos));
            }

            NextChar();
        }
    }

    /// <summary>Parses next characters as an identifier.</summary>
    private void ParseIdentifier()
    {
        Debug.Assert(IsValidStartingCharForIdentifier, "IsValidStartingCharForIdentifier");

        do
        {
            NextChar();
        } while (IsValidNonStartingCharForIdentifier);
    }

    /// <summary>Resolves the <paramref name="tokenType"/> in case identifier is a reserved identifier.</summary>
    private static TokenType ResolveTokenType(TokenType tokenType, string tokenText)
    {
        if (tokenType != TokenType.Identifier)
            return tokenType;

        return tokenText switch
        {
            "true" or "false" => TokenType.Boolean,
            "null" => TokenType.Null,
            _ => tokenType
        };
    }

    /// <summary>
    /// Creates a <see cref="SyntaxException"/> for given <paramref name="message"/> and current lexer state.
    /// </summary>
    /// <param name="message">Message for the exception.</param>
    /// <returns>The <see cref="SyntaxException"/>.</returns>
    private SyntaxException SyntaxException(string message) => new(message, _textPos, _text);
}