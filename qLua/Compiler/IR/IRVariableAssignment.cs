using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable assignment represents a variable assignment in the intermediate representation.
/// </summary>
public sealed class IRVariableAssignment(
    IToken startToken,
    IToken endToken,
    IRVariable name,
    IRExpression? value)
    : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The name of the variable.
    /// </summary>
    public IRVariable Name { get; } = name;

    /// <summary>
    /// The value to assign to the variable.
    /// </summary>
    public IRExpression? Value { get; } = value;
}