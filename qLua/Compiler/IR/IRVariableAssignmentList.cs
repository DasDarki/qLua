using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable assignment list represents a list of variable assignments in the intermediate representation.
/// </summary>
public sealed class IRVariableAssignmentList(IToken startToken, IToken endToken, IList<IRVariableAssignment> assignments)
    : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The list of variable assignments.
    /// </summary>
    public IList<IRVariableAssignment> Assignments { get; } = assignments;
}