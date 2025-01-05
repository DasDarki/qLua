namespace qLua.Compiler;

/// <summary>
/// The qlua compiler is responsible for compiling the qlua source code. The compilation process is split into multiple stages. <br/>
/// 1. AST Generation: The source code is parsed and an abstract syntax tree (AST) is generated. This is done by ANTLR. <br/>
/// 2. IR Generation: The AST is traversed and an intermediate representation (IR) is generated. <br/>
/// 3. Analysis: The IR is analyzed to ensure that it is semantically correct. At this stage type checking is also performed. <br/>
/// 4. Transformation: The IR is transformed into the target format, be it transpiled Lua code or bytecode.
/// </summary>
public sealed class QLuaCompiler
{
    /// <summary>
    /// Starts the compilation process for the given working directory.
    /// </summary>
    /// <param name="workingDir">The working directory to compile.</param>
    /// <param name="config">The configuration to use for the compilation.</param>
    /// <returns>The result of the compilation process.</returns>
    public static CompilationResult Execute(string workingDir, Config config)
    {
        var result = new CompilationResult();

        return result;
    }
}