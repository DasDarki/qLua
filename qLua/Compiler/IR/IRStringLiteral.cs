using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR string literal represents a string value in the intermediate representation.
/// </summary>
public sealed class IRStringLiteral(IToken startToken, IToken endToken, string startQuote, string endQuote, string value) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The start quote string used to delimit the string at the start.
    /// </summary>
    public string StartQuote { get; } = startQuote;
    
    /// <summary>
    /// The end quote string used to delimit the string at the end.
    /// </summary>
    public string EndQuote { get; } = endQuote;
    
    /// <summary>
    /// The value of the string literal.
    /// </summary>
    public string Value { get; } = value;
}