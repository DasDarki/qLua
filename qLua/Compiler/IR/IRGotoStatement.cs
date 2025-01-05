using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR goto statement represents a goto statement in the intermediate representation.
/// </summary>
public sealed class IRGotoStatement(IToken startToken, IToken endToken, string label) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The label to jump to.
    /// </summary>
    public string Label { get; } = label;
}