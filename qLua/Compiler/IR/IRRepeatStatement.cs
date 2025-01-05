using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR repeat statement represents a repeat statement in the intermediate representation.
/// </summary>
public sealed class IRRepeatStatement(IToken startToken, IToken endToken, IRExpression condition, IRBlock body) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The condition of the repeat statement.
    /// </summary>
    public IRExpression Condition { get; } = condition;

    /// <summary>
    /// The body of the repeat statement.
    /// </summary>
    public IRBlock Body { get; } = body;
}