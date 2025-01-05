using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable declaration list represents a list of variable declarations in the intermediate representation.
/// </summary>
public sealed class IRVariableDeclarationList(IToken startToken, IToken endToken, IList<IRVariableDeclaration> variables)
    : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The variable declarations in the list.
    /// </summary>
    public IList<IRVariableDeclaration> Variables { get; } = variables;
}