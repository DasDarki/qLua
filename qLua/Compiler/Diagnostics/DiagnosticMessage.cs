namespace qLua.Compiler.Diagnostics;

/// <summary>
/// A attribute for diagnostic messages that can be used to document the diagnostic code.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class DiagnosticMessage : Attribute
{
    public string Message { get; }
    
    public DiagnosticMessage(string message)
    {
        Message = message;
    }
}