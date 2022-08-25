using LuaInterface;
using UnityEngine;

namespace FrameWork.Manager
{
    public class LuaMgr:BaseManager
    {
        private LuaState _lua;
        private LuaLooper _looper;
        private GameObject looper_go;
        private LuaFunction _luaFunc;
        public LuaState Lua
        {
            get
            {
                if (_lua == null)
                {
                    Debug.LogError("LuaManager is not Init");
                }
                return _lua;
            }
        }

        public override void Init()
        {
            _lua=new LuaState();
            _lua.LuaSetTop(0);
            LuaBinder.Bind(_lua);
            _lua.Start();
            
            //运行main.lua加载基础lua脚本
            StartMain();
            StartLooper();
        }

        private void StartMain()
        {
            _lua.DoFile("main.lua");
        }

        private void StartLooper()
        {
            looper_go =new GameObject("LuaLooper");
            GameObject.DontDestroyOnLoad(looper_go);
            _looper = looper_go.AddComponent<LuaLooper>();
            _looper.luaState = Lua;
        }

        public override void Destroy()
        {
            _lua.CheckTop();
            _lua.Dispose();
        }

        public void CallLuaFunction(string func)
        {
            _luaFunc = null;
            Debug.Log(func);
            _luaFunc = _lua.GetFunction(func);
            if (_luaFunc != null)
            {
                _luaFunc.Call();
            }
        }
    }
}