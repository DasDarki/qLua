namespace qLua.Compiler;

/// <summary>
/// An enum containing all the supported Lua versions for the compiler.
/// </summary>
public enum LuaVersion
{
    /// <summary>
    /// The Lua 5.1 version. For backwards compatibility, polyfills are used to support older versions but some features
    /// may not be available, the compiler will fail if it detects that a feature is not available.
    /// </summary>
    Lua51 = 51,
    /// <summary>
    /// The Lua 5.4 version. This is the default version.
    /// </summary>
    Lua54 = 54
}