using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR if statement represents an if statement in the intermediate representation.
/// </summary>
public sealed class IRIfStatement(
    IToken startToken,
    IToken endToken,
    IRExpression condition,
    IRBlock body,
    IDictionary<IRExpression, IRBlock> elseIfs,
    IRBlock? elseBody) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The condition of the if statement.
    /// </summary>
    public IRExpression Condition { get; } = condition;

    /// <summary>
    /// The body of the if statement.
    /// </summary>
    public IRBlock Body { get; } = body;

    /// <summary>
    /// A dictionary of else-if conditions and bodies.
    /// </summary>
    public IDictionary<IRExpression, IRBlock> ElseIfs { get; } = elseIfs;

    /// <summary>
    /// The body of the else statement. This is null if there is no else statement.
    /// </summary>
    public IRBlock? ElseBody { get; } = elseBody;
}