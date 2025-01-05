using Antlr4.Runtime;

namespace qLua.Compiler.IR;

/// <summary>
/// The IR table literal represents a table literal/initializer in the intermediate representation.
/// </summary>
public sealed class IRTableLiteral(IToken startToken, IToken endToken, params IEnumerable<KeyValuePair<IRExpression, IRExpression>>[] fields) : IRExpression(startToken, endToken)
{
    /// <summary>
    /// A dictionary of the fields of the table literal.
    /// </summary>
    public IDictionary<IRExpression, IRExpression> Fields { get; } = fields.SelectMany(f => f).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
}