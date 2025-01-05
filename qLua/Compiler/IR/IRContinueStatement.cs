using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR continue statement represents the continue statement in the intermediate representation.
/// </summary>
public sealed class IRContinueStatement(IToken startToken, IToken endToken, int depth) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The depth of the continue statement.
    /// </summary>
    public int Depth { get; } = depth;
}