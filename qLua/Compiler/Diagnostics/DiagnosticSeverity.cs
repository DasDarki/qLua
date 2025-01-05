namespace qLua.Compiler.Diagnostics;

/// <summary>
/// The diagnostic severity is the severity of a diagnostic.
/// </summary>
public enum DiagnosticSeverity
{
    /// <summary>
    /// An info occurred. This is the lowest severity and is used for given the user information like recommendations.
    /// </summary>
    Info,
    /// <summary>
    /// A warning occured. This is the second lowest severity and is used for giving the user a hint that something
    /// should definitely be changed because it might cause problems.
    /// </summary>
    Warning,
    /// <summary>
    /// An error occurred. This is the highest severity and is used for giving the user a hint that something is
    /// really wrong and needs to be changed. The compilation will fail if an error occurred.
    /// </summary>
    Error
}