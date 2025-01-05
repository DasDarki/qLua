using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR unary expression represents a unary expression in the intermediate representation.
/// </summary>
public class IRUnaryExpression(IToken startToken, IToken endToken, string @operator, IRExpression operand) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The operator of the unary expression.
    /// </summary>
    public string Operator { get; } = @operator;
    
    /// <summary>
    /// The operand of the unary expression.
    /// </summary>
    public IRExpression Operand { get; } = operand;
}