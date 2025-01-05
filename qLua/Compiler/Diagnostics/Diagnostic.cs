using Antlr4.Runtime;

namespace qLua.Compiler.Diagnostics;

/// <summary>
/// The diagnostic is an object holding information about an error or warning that occurred during the compilation.
/// </summary>
public sealed class Diagnostic
{
    /// <summary>
    /// The start token where the diagnostic occurred.
    /// </summary>
    public IToken StartToken { get; }
    
    /// <summary>
    /// The end token where the diagnostic occurred.
    /// </summary>
    public IToken EndToken { get; }
    
    /// <summary>
    /// The severity of the diagnostic.
    /// </summary>
    public DiagnosticSeverity Severity { get; }
    
    /// <summary>
    /// The code of the diagnostic.
    /// </summary>
    public DiagnosticCode Code { get; }
    
    /// <summary>
    /// The message of the diagnostic.
    /// </summary>
    public string Message { get; }
    
    internal Diagnostic(IToken startToken, IToken endToken, DiagnosticSeverity severity, DiagnosticCode code, string message)
    {
        StartToken = startToken;
        EndToken = endToken;
        Severity = severity;
        Code = code;
        Message = message;
    }
}