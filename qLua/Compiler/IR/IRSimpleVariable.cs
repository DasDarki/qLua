using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR simple variable represents a simple variable in the intermediate representation.
/// </summary>
public sealed class IRSimpleVariable(IToken startToken, IToken endToken, string name) : IRVariable(startToken, endToken)
{
    /// <summary>
    /// The name of the variable.
    /// </summary>
    public string Name { get; } = name;
}