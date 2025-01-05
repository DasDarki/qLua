using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR for statement represents a for statement in the intermediate representation.
/// </summary>
public sealed class IRForStatement(
    IToken startToken,
    IToken endToken,
    IRVariable variable,
    IRExpression start,
    IRExpression end,
    IRExpression? step,
    IRBlock block) : IRStatement(startToken, endToken)
{
    /// <summary>
    /// The variable to use in the for statement.
    /// </summary>
    public IRVariable Variable { get; } = variable;
    
    /// <summary>
    /// The start expression of the for statement.
    /// </summary>
    public IRExpression Start { get; } = start;
    
    /// <summary>
    /// The end expression of the for statement.
    /// </summary>
    public IRExpression End { get; } = end;
    
    /// <summary>
    /// The step expression of the for statement. Can be null if not specified which means 1.
    /// </summary>
    public IRExpression? Step { get; } = step;
    
    /// <summary>
    /// The block of the statement.
    /// </summary>
    public IRBlock Block { get; } = block;
}