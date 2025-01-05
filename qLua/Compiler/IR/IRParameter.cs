using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR parameter represents a parameter in the intermediate representation.
/// </summary>
public sealed class IRParameter(IToken startToken, IToken endToken, string name, IRExpression? defaultValue = null, IRTypeAnnotation? typeDeclaration = null)
{
    /// <summary>
    /// The name of the parameter.
    /// </summary>
    public string Name { get; } = name;
    
    /// <summary>
    /// The default value of the parameter.
    /// </summary>
    public IRExpression? DefaultValue { get; } = defaultValue;
    
    /// <summary>
    /// The type declaration of the parameter.
    /// </summary>
    public IRTypeAnnotation? Type { get; } = typeDeclaration;
}