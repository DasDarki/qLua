using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR while statement represents a while loop in the intermediate representation.
/// </summary>
public sealed class IRWhileStatement(IToken startToken, IToken endToken, IRExpression condition, IRBlock statements) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The condition of the while loop.
    /// </summary>
    public IRExpression Condition { get; } = condition;
    
    /// <summary>
    /// The block of statements of the while loop.
    /// </summary>
    public IRBlock Block { get; } = statements;
}