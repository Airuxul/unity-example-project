namespace GameApp.ToLuaExpand
{
    public interface ILuaValueBinder
    {
        BindableValue Val { get; }
        string LuaPath { get; }
        bool HaveInit { get; set; }
    }
}