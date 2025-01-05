using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR number literal represents a number value in the intermediate representation.
/// </summary>
public sealed class IRNumberLiteral(IToken startToken, IToken endToken, double value, bool isHexdecimal) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The value of the number literal.
    /// </summary>
    public double Value { get; } = value;
    
    /// <summary>
    /// Indicates whether the number literal is a hexdecimal number.
    /// </summary>
    public bool IsHexdecimal { get; } = isHexdecimal;
}