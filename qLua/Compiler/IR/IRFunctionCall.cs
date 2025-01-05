using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR function call variable represents a function call in the intermediate representation.
/// </summary>
public sealed class IRFunctionCall(IToken startToken, IToken endToken, IRVariable function, IList<IRExpression> arguments) : IRVariable(startToken, endToken)
{
    /// <summary>
    /// The function variable that is called.
    /// </summary>
    public IRVariable Function { get; } = function;
    
    /// <summary>
    /// The arguments of the function call.
    /// </summary>
    public IList<IRExpression> Arguments { get; } = arguments;
}