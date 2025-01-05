using qLua.Compiler.Diagnostics;

namespace qLua;

/// <summary>
/// The internal logger used by the compiler to log information, warnings and errors.
/// </summary>
internal static class Logger
{
    /// <summary>
    /// Prints a warning message to the console.
    /// </summary>
    public static void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("<WARN>\t");
        Console.ResetColor();
        Console.WriteLine(message);
    }
    
    /// <summary>
    /// Prints a success message to the console.
    /// </summary>
    public static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("<SUCCESS>\t");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    /// <summary>
    /// Prints the diagnostics result of the compilation process to the console.
    /// </summary>
    /// <param name="diagnostics">The diagnostics bag containing all diagnostics that were emitted during the compilation process.</param>
    public static void LogDiagnostics(DiagnosticsBag diagnostics)
    {
        
    }
}