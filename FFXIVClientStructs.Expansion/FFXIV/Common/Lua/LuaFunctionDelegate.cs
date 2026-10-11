using FFXIVClientStructs.FFXIV.Common.Lua;

namespace FFXIVClientStructs.Expansion.FFXIV.Common.Lua;

public unsafe delegate ulong LuaFunctionDelegate
(
    lua_State* state
);
