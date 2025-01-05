using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR boolean literal represents a boolean value in the intermediate representation.
/// </summary>
public sealed class IRBooleanLiteral(IToken startToken, IToken endToken, bool value) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The value of the boolean literal.
    /// </summary>
    public bool Value { get; } = value;
}