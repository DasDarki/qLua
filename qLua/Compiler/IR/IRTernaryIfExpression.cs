using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR ternary if expression represents an if expression in the intermediate representation.
/// For example, in the expression "a ? b : c", "a" is the condition, "b" is the then expression and "c" is the else expression.
/// </summary>
public sealed class IRTernaryIfExpression(IToken startToken, IToken endToken, IRExpression condition, IRExpression thenExpression, IRExpression elseExpression) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// The condition of the ternary if expression.
    /// </summary>
    public IRExpression Condition { get; } = condition;
    
    /// <summary>
    /// The then expression of the ternary if expression.
    /// </summary>
    public IRExpression ThenExpression { get; } = thenExpression;
    
    /// <summary>
    /// The else expression of the ternary if expression.
    /// </summary>
    public IRExpression ElseExpression { get; } = elseExpression;
}