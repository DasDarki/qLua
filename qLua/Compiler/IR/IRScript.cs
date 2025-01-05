namespace qLua.Compiler.IR;

/// <summary>
/// The IR script represents a whole script in the intermediate representation.
/// </summary>
public sealed class IRScript(IRBlock block) : IRBlock(block.StartToken, block.EndToken, block.Statements);