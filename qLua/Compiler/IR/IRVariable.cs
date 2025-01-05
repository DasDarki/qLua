using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR variable represents the base class for all variables in the intermediate representation.
/// Variables can be either normal variables like local name = value or table fields like table.name = value or
/// even array fields like table[1] = value or function calls like func() = value.
/// </summary>
public abstract class IRVariable(IToken startToken, IToken endToken) : IRExpression(startToken, endToken);