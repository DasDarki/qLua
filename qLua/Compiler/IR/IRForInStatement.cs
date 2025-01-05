using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR for-in statement represents a for-in statement in the intermediate representation.
/// </summary>
public sealed class IRForInStatement(
    IToken startToken,
    IToken endToken,
    IList<IRVariable> variables,
    IList<IRExpression> expressions,
    IRBlock body) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The variables to assign the values to.
    /// </summary>
    public IList<IRVariable> Variables { get; } = variables;

    /// <summary>
    /// The expressions to get the values from.
    /// </summary>
    public IList<IRExpression> Expressions { get; } = expressions;

    /// <summary>
    /// The block of the statement.
    /// </summary>
    public IRBlock Block { get; } = body;
}