using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR label statement represents a label in the intermediate representation.
/// </summary>
public sealed class IRLabelStatement(IToken startToken, IToken endToken, string label) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The label text of the statement.
    /// </summary>
    public string Label { get; } = label;
}