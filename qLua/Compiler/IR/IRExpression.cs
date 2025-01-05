using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR expression is the base class for all intermediate representation expressions.
/// </summary>
public class IRExpression(IToken startToken, IToken endToken) : IRNode(startToken, endToken);