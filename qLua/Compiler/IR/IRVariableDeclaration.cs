using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable declaration represents a variable declaration in the intermediate representation.
/// </summary>
public sealed class IRVariableDeclaration(
    IToken startToken,
    IToken endToken,
    bool isLocal,
    IRVariableName name,
    IRExpression? initializer = null)
    : IRStatement(startToken, endToken)
{
    /// <summary>
    /// Whether the variable is local.
    /// </summary>
    public bool IsLocal { get; } = isLocal;

    /// <summary>
    /// The name of the variable.
    /// </summary>
    public IRVariableName Name { get; } = name;

    /// <summary>
    /// The initializer of the variable. If the variable is not initialized, this is null.
    /// </summary>
    public IRExpression? Initializer { get; } = initializer;

}