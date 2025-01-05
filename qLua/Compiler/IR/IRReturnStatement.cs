using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR return statement represents a return statement in the intermediate representation.
/// </summary>
public sealed class IRReturnStatement(IToken startToken, IToken endToken) : IRStatement(startToken, endToken);