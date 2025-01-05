using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable name represents a variable name in the intermediate representation.
/// The variable name defines the name of a variable, the type of the variable and an optional attribute (e.g. &lt;const&&gt;).
/// </summary>
public sealed class IRVariableName(IToken startToken, IToken endToken, string name, IRTypeAnnotation? type = null, string? attribute = null) : IRNode(startToken, endToken)
{
    /// <summary>
    /// The name of the variable.
    /// </summary>
    public string Name { get; } = name;
    
    /// <summary>
    /// The type of the variable. If the type is not specified, this is null and any is assumed.
    /// </summary>
    public IRTypeAnnotation? Type { get; } = type;
    
    /// <summary>
    /// The attribute of the variable. If the attribute is not specified, this is null.
    /// </summary>
    public string? Attribute { get; } = attribute;
}