using FrameWork;
using LuaInterface;
using UnityEngine;

namespace GameApp.ToLuaExpand
{
    public static class LuaValueBinderImpl
    {
        private static LuaState _lua;
        public static LuaState Lua
        {
            get
            {
                if (_lua == null)
                {
                    _lua = AppFacade.LuaMgr.Lua != null ? AppFacade.LuaMgr.Lua : null;
                    if(_lua==null)
                        Debug.LogError("LuaManager's LuaState is null");
                }
                return _lua;
            }
        }

        private static LuaFunction _initFunc;
        private static LuaFunction InitFunc
        {
            get
            {
                if (_initFunc == null)
                {
                    _initFunc = Lua?.GetFunction("Adapter.Init");
                    if(_initFunc==null)
                        Debug.LogError("Can't find Adapter.lua's Init func");
                }
                return _initFunc;
            }
        }

        private static LuaFunction _callFunc;

        private static LuaFunction CallFunc
        {
            get
            {
                if (_callFunc == null)
                {
                    _callFunc = Lua?.GetFunction("Adapter.Call");
                    if(_callFunc==null)
                        Debug.LogError("Can't find Adapter.lua's Init func");
                }
                return _callFunc;
            }
        }
        
        //调用Adapter.lua脚本的Init方法实现Lua信息载入
        public static void Init(this ILuaValueBinder binder)
        {
            if (binder.HaveInit || Lua==null || string.IsNullOrEmpty(binder.LuaPath)) return;
            InitFunc?.Call(binder, binder.LuaPath);
            binder.HaveInit = true;
        }

        public static void CallLuaFunc(this ILuaValueBinder binder, string method)
        {
            if (!binder.HaveInit) return;
            CallFunc?.Call(binder,method);
        }

        public static void CallLuaFunc<T>(this ILuaValueBinder binder, string method, T t)
        {
            if (!binder.HaveInit) return;
            CallFunc?.Call(binder,method,t);
        }
        
    }
}