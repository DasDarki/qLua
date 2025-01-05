using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR do statement represents a do block in the intermediate representation.
/// </summary>
public sealed class IRDoStatement(IToken startToken, IToken endToken, IRBlock block) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The block of the statement.
    /// </summary>
    public IRBlock Block { get; } = block;
}