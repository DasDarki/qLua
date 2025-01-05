using Antlr4.Runtime;
using qLua.Compiler;
using qLua.Compiler.CodeAnalysis;
using qLua.Compiler.Diagnostics;

var tokenStream = new AntlrFileStream(@"E:\Development\Small Projects\qLua\qLua\sample.qlua");
var lexer = new QLuaLexer(tokenStream);
var parser = new QLuaParser(new CommonTokenStream(lexer));
var diagnostics = new DiagnosticsBag();
var visitor = new QLuaIRVisitor(diagnostics);
var script = visitor.VisitChunk(parser.chunk());

Console.WriteLine();

/*Console.Title = $"qlua v{Assembly.GetExecutingAssembly().GetName().Version} by DasDarki";
var command = ParseCommand(args);
switch (command)
{
    case Command.Config:
        Config.GenerateDefaults(Environment.CurrentDirectory);
        Logger.Success("Generated default config file!");
        break;
    case Command.Version:
        Console.WriteLine($"qlua v{Assembly.GetExecutingAssembly().GetName().Version}");
        break;
    case Command.Compile:
        var currentDir = Environment.CurrentDirectory;
        var config = Config.Load(currentDir);
        if (!Config.Exists(currentDir))
        {
            Logger.Warn("No config file found. Using default config. Waiting 5 seconds to continue...");
            Thread.Sleep(5000);
        }
        
        var result = QLuaCompiler.Execute(currentDir, config);
        Logger.LogDiagnostics(result.Diagnostics);
        break;
    case null:
        Console.WriteLine("Invalid command.");
        Usage();
        return;
    case Command.Help:
        Usage();
        return;
    default:
        throw new ArgumentOutOfRangeException();
}

Console.WriteLine();
return;

void Usage()
{
    Console.WriteLine("Usage: qlua [options]");
    Console.WriteLine("Options:");
    Console.WriteLine("  -h, --help\tDisplays this help message.");
    Console.WriteLine("  -cfg, --config\tGenerates a default config file at the current directory.");
    Console.WriteLine("  -v, --version\tDisplays the version of the compiler.");
    Console.WriteLine("");
    Console.WriteLine("Leave out any options to compile the project at the current directory.");
}

Command? ParseCommand(IReadOnlyList<string> args)
{
    return args.Count switch
    {
        0 => Command.Compile,
        1 when args[0] == "-h" || args[0] == "--help" => Command.Help,
        1 when args[0] == "-cfg" || args[0] == "--config" => Command.Config,
        1 when args[0] == "-v" || args[0] == "--version" => Command.Version,
        _ => null
    };
}

internal enum Command
{
    Help, Config, Version, Compile
}*/