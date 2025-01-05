using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR field access variable represents a field access in the intermediate representation.
/// Meant is any access like table.name or table:method.
/// </summary>
public sealed class IRFieldAccessVariable(IToken startToken, IToken endToken, IRVariable @base, string fieldName, char delimiter = '.') : IRVariable(startToken, endToken)
{
    /// <summary>
    /// The base variable of the field access. This is the table or array that is accessed.
    /// </summary>
    public IRVariable Base { get; } = @base;

    /// <summary>
    /// The name of the field.
    /// </summary>
    public string FieldName { get; } = fieldName;
    
    /// <summary>
    /// The delimiter used to access the field. This can be either '.' or ':'.
    /// </summary>
    public char Delimiter { get; } = delimiter;
}