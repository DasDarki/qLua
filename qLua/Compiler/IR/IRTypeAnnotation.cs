using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR type annotation represents a type annotation in the intermediate representation.
/// </summary>
public sealed class IRTypeAnnotation(IToken startToken, IToken endToken) : IRNode(startToken, endToken)
{
    
}