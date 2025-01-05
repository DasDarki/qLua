using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR binary expression represents a binary expression in the intermediate representation.
/// </summary>
public class IRBinaryExpression(IToken startToken, IToken endToken, string @operator, IRExpression left, IRExpression right) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The operator of the binary expression.
    /// </summary>
    public string Operator { get; } = @operator;
    
    /// <summary>
    /// The left-hand side of the binary expression.
    /// </summary>
    public IRExpression Left { get; } = left;
    
    /// <summary>
    /// The right-hand side of the binary expression.
    /// </summary>
    public IRExpression Right { get; } = right;
}