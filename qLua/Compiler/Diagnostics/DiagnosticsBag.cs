using System.Reflection;
using Antlr4.Runtime;

namespace qLua.Compiler.Diagnostics;

/// <summary>
/// The diagnostic bag is a collection of diagnostics that can be used to report errors, warnings and information.
/// </summary>
public sealed class DiagnosticsBag
{
    /// <summary>
    /// A read-only list of all diagnostics.
    /// </summary>
    public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics.AsReadOnly();
    
    private readonly List<Diagnostic> _diagnostics = new();
    private readonly Dictionary<DiagnosticCode, string> _messages = new();

    internal DiagnosticsBag()
    {
        var enumType = typeof(DiagnosticCode);
        
        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<DiagnosticMessage>();
            if (attribute != null)
            {
                _messages.Add((DiagnosticCode)field.GetValue(null)!, attribute.Message);
            }
        }
    }
    
    /// <summary>
    /// Reports a diagnostic.
    /// </summary>
    /// <param name="startToken">The start token of the diagnostic.</param>
    /// <param name="endToken">The end token of the diagnostic.</param>
    /// <param name="severity">The severity of the diagnostic.</param>
    /// <param name="code">The code of the diagnostic.</param>
    /// <param name="args">The arguments of the diagnostic.</param>
    public void ReportDiagnostic(IToken startToken, IToken endToken, DiagnosticSeverity severity, DiagnosticCode code, params object[] args)
    {
        var message = _messages.TryGetValue(code, out var predefinedMessage) ? string.Format(predefinedMessage, args) : "Unknown diagnostic code.";
        var diagnostic = new Diagnostic(startToken, endToken, severity, code, message);
        _diagnostics.Add(diagnostic);
    }
    
    /// <summary>
    /// Reports an information diagnostic.
    /// </summary>
    /// <param name="startToken">The start token of the diagnostic.</param>
    /// <param name="endToken">The end token of the diagnostic.</param>
    /// <param name="code">The code of the diagnostic.</param>
    /// <param name="args">The arguments of the diagnostic.</param>
    public void ReportInfo(IToken startToken, IToken endToken, DiagnosticCode code, params object[] args)
    {
        ReportDiagnostic(startToken, endToken, DiagnosticSeverity.Info, code, args);
    }
    
    /// <summary>
    /// Reports a warning diagnostic.
    /// </summary>
    /// <param name="startToken">The start token of the diagnostic.</param>
    /// <param name="endToken">The end token of the diagnostic.</param>
    /// <param name="code">The code of the diagnostic.</param>
    /// <param name="args">The arguments of the diagnostic.</param>
    public void ReportWarning(IToken startToken, IToken endToken, DiagnosticCode code, params object[] args)
    {
        ReportDiagnostic(startToken, endToken, DiagnosticSeverity.Warning, code, args);
    }
    
    /// <summary>
    /// Reports an error diagnostic.
    /// </summary>
    /// <param name="startToken">The start token of the diagnostic.</param>
    /// <param name="endToken">The end token of the diagnostic.</param>
    /// <param name="code">The code of the diagnostic.</param>
    /// <param name="args">The arguments of the diagnostic.</param>
    public void ReportError(IToken startToken, IToken endToken, DiagnosticCode code, params object[] args)
    {
        ReportDiagnostic(startToken, endToken, DiagnosticSeverity.Error, code, args);
    }
}