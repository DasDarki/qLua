using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR statement is the base class for all intermediate representation statements.
/// </summary>
public class IRStatement(IToken startToken, IToken endToken) : IRNode(startToken, endToken);