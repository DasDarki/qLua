using qLua.Compiler.Diagnostics;

namespace qLua.Compiler;

/// <summary>
/// The compilation result is used to store the result of a compilation process.
/// </summary>
public sealed class CompilationResult()
{
    /// <summary>
    /// The diagnostics bag containing all diagnostics that were emitted during the compilation process.
    /// </summary>
    public DiagnosticsBag Diagnostics { get; } = new();
    
    /// <summary>
    /// The result of the compilation process. It can contain the transpiled code or compiled binary.
    /// Can be null, if the compilation process failed.
    /// </summary>
    public byte[]? Result { get; set; }
}