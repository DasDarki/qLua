using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR nil literal represents the nil value in the intermediate representation.
/// </summary>
public sealed class IRNilLiteral(IToken startToken, IToken endToken) : IRExpression(startToken, endToken);