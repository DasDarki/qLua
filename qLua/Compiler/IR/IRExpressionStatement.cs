using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR expression statement represents an expression as a statement in the intermediate representation.
/// </summary>
public class IRExpressionStatement(IToken startToken, IToken endToken, IRExpression expression) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The expression of the statement.
    /// </summary>
    public IRExpression Expression { get; } = expression;
}