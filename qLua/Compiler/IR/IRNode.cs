using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR node is the base class for all intermediate representation nodes.
/// </summary>
public abstract class IRNode(IToken startToken, IToken endToken)
{
    /// <summary>
    /// The start token of the node.
    /// </summary>
    public IToken StartToken { get; } = startToken;

    /// <summary>
    /// The end token of the node.
    /// </summary>
    public IToken EndToken { get; } = endToken;
}