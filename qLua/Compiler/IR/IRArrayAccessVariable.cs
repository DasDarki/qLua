using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR array access variable represents an array access in the intermediate representation.
/// </summary>
public sealed class IRArrayAccessVariable(IToken startToken, IToken endToken, IRVariable @base, IRExpression index) : IRVariable(startToken, endToken)
{
    /// <summary>
    /// The base variable of the array access. This is the table or array that is accessed.
    /// </summary>
    public IRVariable Base { get; } = @base;

    /// <summary>
    /// The index of the array access.
    /// </summary>
    public IRExpression Index { get; } = index;
}