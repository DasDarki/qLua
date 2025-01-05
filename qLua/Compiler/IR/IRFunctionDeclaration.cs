using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR function declaration represents a function declaration in the intermediate representation.
/// </summary>
public sealed class IRFunctionDeclaration(
    IToken startToken,
    IToken endToken,
    bool isLocal,
    string name,
    IList<IRParameter> parameters,
    IRTypeAnnotation? returnType,
    IRBlock block)
    : IRNode(startToken, endToken)
{
    /// <summary>
    /// Whether the function is local.
    /// </summary>
    public bool IsLocal { get; } = isLocal;

    /// <summary>
    /// The name of the function. This can contain dots and colons for nested functions.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The parameters of the function.
    /// </summary>
    public IList<IRParameter> Parameters { get; } = parameters;

    /// <summary>
    /// The return type of the function. If empty, any is assumed.
    /// </summary>
    public IRTypeAnnotation? ReturnType { get; } = returnType;

    /// <summary>
    /// The block of the statement.
    /// </summary>
    public IRBlock Block { get; } = block;
}