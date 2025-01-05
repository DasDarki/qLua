using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR break statement represents a break statement in the intermediate representation.
/// </summary>
public sealed class IRBreakStatement(IToken startToken, IToken endToken, int depth) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The depth of the break statement.
    /// </summary>
    public int Depth { get; } = depth;
}