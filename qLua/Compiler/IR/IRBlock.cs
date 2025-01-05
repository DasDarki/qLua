using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR block represents a block of statements in the intermediate representation.
/// </summary>
public class IRBlock(IToken startToken, IToken endToken, IList<IRStatement> statements) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The list of statements in the block.
    /// </summary>
    public IList<IRStatement> Statements { get; } = statements;
}