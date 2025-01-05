namespace qLua.Compiler.Diagnostics;

/// <summary>
/// This enum contains ALL warnings, errors and information codes. The format is as follows:
/// <para>
/// <li>The first three letters describe the type of the diagnostic. INF for information, WRN for warning and ERR for error.</li>
/// <li>The next three letters describes the category of the diagnostic. PRS stands for "Parsing", SEM stands for "Semantic" and TYP stands for "Type Checking".</li>
/// <li>The last three digits describe the specific problem. For example, ERR_SYM_001 means that there is a syntax error in the code.</li>
/// </para>
/// </summary>
public enum DiagnosticCode
{
    [DiagnosticMessage("When parsing a block, a statement was not a valid statement.")]
    ERR_PRS_000,
    
    [DiagnosticMessage("When parsing a variable, the base was not an expression but was expected to be.")]
    ERR_PRS_001,
    
    [DiagnosticMessage("When parsing a variable, the base was not a valid variable.")]
    ERR_PRS_002,
    
    [DiagnosticMessage("When parsing a variable, the format was not correct.")]
    ERR_PRS_003,
    
    [DiagnosticMessage("When parsing a variable, the index expression for array access was not a valid expression.")]
    ERR_PRS_004,
    
    [DiagnosticMessage("When parsing a variable, an argument expression in a function call was not a valid expression.")]
    ERR_PRS_005,
    
    [DiagnosticMessage("When parsing a for-in statement, an iterable expression was not a valid expression.")]
    ERR_PRS_006,
    
    [DiagnosticMessage("When parsing a for-in statement, the number of variables does not match the number of expressions. Expected {0} but got {1}.")]
    ERR_PRS_007
}