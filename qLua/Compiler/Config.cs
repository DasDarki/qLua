using Tomlet;
using Tomlet.Attributes;

namespace qLua.Compiler;

/// <summary>
/// The configuration object contains information about the current configuration of the compiler and how it should behave.
/// The configuration file is a TOML file that is loaded by the compiler and parsed into this object. It must be at the root
/// directory of the project and must be named "qLua-conf.toml".
/// </summary>
public sealed class Config
{
    [TomlPrecedingComment("The version of Lua that the compiler should target. Defaults to Lua 5.4. Allowed options are Lua51 and Lua54.")]
    [TomlProperty("luav")]
    public LuaVersion LuaVersion { get; set; } = LuaVersion.Lua54;
    
    [TomlPrecedingComment("The directory that contains the source files. It must only contain the root directory of the project. Defaults to \"src\".")]
    [TomlProperty("srcDir")]
    public string SourceDirectory { get; set; } = "src";
    
    [TomlPrecedingComment("The name (without extension!) of the entry file that should be compiled first. If empty the compiler starts mostly at the top of the folder. Defaults to \"\".")]
    [TomlProperty("entry")]
    public string EntryFile { get; set; } = "";

    [TomlPrecedingComment("An array of patterns that should be ignored when compiling. Defaults to an empty array.")] 
    [TomlProperty("ignored")]
    public string[] IgnoredPatterns { get; set; } = [];
    
    [TomlPrecedingComment("The output configuration of the compiler.")]
    [TomlProperty("output")]
    public OutputSection Output { get; set; } = new();

    [TomlPrecedingComment("The rules that should be used when compiling.")]
    [TomlProperty("rules")]
    public RulesSection Rules { get; set; } = new();
    
    [TomlPrecedingComment("The mixins that should be used when compiling.")]
    [TomlProperty("mixins")]
    public MixinsSection Mixins { get; set; } = new();
    
    public sealed class RulesSection
    {
        [TomlPrecedingComment("Whether or not the compiler should allow implicit anys. Implicit anys are anys that are not explicitly specified. Defaults to false.")]
        [TomlProperty("allowImplicitAny")]
        public bool AllowImplicitAny { get; set; } = false;
        
        [TomlPrecedingComment("Whether any is allowed in general or all values must strictly define their type. Defaults to false.")]
        [TomlProperty("strictlyTyped")]
        public bool StrictlyTyped { get; set; } = false;
        
        [TomlPrecedingComment("Defines which index base should be used when indexing tables. Defaults to 1.")]
        [TomlProperty("indexBase")]
        public int IndexBase { get; set; } = 1;
        
        [TomlPrecedingComment("Whether the compiler should allow nilable values or nil in general. Defaults to true.")]
        [TomlProperty("allowNil")]
        public bool AllowNil { get; set; } = true;

        /// <summary>
        /// Gets the index offset that should be used when indexing tables.
        /// </summary>
        public int GetIndexOffset() => (IndexBase - 1) * -1;
    }
    
    public sealed class OutputSection
    {
        [TomlPrecedingComment("The directory that contains the output files. Defaults to \"out\".")]
        [TomlProperty("dir")]
        public string OutputDirectory { get; set; } = "out";
        
        [TomlPrecedingComment("Whether the output should be minified or not. Defaults to false.")]
        [TomlProperty("minify")]
        public bool Minify { get; set; } = false;
        
        [TomlPrecedingComment("The tab size measured in spaces that should be used when formatting the output. Defaults to 4. Only applies when minify is false and useSpacesAsIndentation is true.")]
        [TomlProperty("tabSize")]
        public int TabSize { get; set; } = 4;
        
        [TomlPrecedingComment("Whether the output should use spaces as indentation or not. Defaults to true.")]
        [TomlProperty("useSpacesAsIndentation")]
        public bool UseSpacesAsIndentation { get; set; } = true;
    }
    
    public sealed class MixinsSection
    {
        [TomlPrecedingComment("A mixin allowing to define custom require function which imports another lua file into scope. Defaults to \"require\".")]
        [TomlProperty("customRequire")]
        public string CustomRequire { get; set; } = "require";
    }

    /// <summary>
    /// Generates a default config file at the given directory.
    /// </summary>
    /// <param name="dir">The directory to generate the config file at.</param>
    internal static void GenerateDefaults(string dir)
    {
        var config = new Config();
        var toml = TomletMain.TomlStringFrom(config);
        File.WriteAllText(Path.Combine(dir, "qLua-conf.toml"), toml);
    }
    
    /// <summary>
    /// Loads the config file at the given directory.
    /// </summary>
    /// <param name="dir">The directory to load the config file from.</param>
    /// <returns>The loaded config file or a default config file if the config file does not exist.</returns>
    internal static Config Load(string dir)
    {
        var configPath = Path.Combine(dir, "qLua-conf.toml");
        if (!File.Exists(configPath))
        {
            return new Config(); // This case will be handled by the CLI tool which will print an warning, that the default config was used.
        }

        var toml = File.ReadAllText(configPath);
        var config = TomletMain.To<Config>(toml);
        return config;
    }
    
    /// <summary>
    /// Whether or not a config file exists at the given directory.
    /// </summary>
    internal static bool Exists(string dir)
    {
        var configPath = Path.Combine(dir, "qLua-conf.toml");
        return File.Exists(configPath);
    }
}