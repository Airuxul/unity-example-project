using System.Collections.Generic;

namespace GameApp.ToLuaExpand
{
    public interface ILuaValueBinder
    {
        List<AbstractValue> Val { get; }
        string LuaPath { get; }
        bool HaveInit { get; set; }
    }
}