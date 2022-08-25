using System;
using UnityEngine;

namespace GameApp.ToLuaExpand
{
    public class LuaBehaviour : MonoBehaviour, ILuaValueBinder
    {
        [SerializeField] private string _luaPath;
        public string LuaPath => _luaPath;
        
        [SerializeField] private BindableValue _val;
        public BindableValue Val => _val;
        
        public bool HaveInit { get; set; }

        private void Awake()
        {
            this.Init();
            this.CallLuaFunc("Awake");
        }

        private void Start()
        {
            this.CallLuaFunc("Start");
        }

        private void OnEnable()
        {
            Debug.LogError("OnEnable");
            this.CallLuaFunc("OnEnable");
        }

        private void OnDisable()
        {
            Debug.LogError("OnDisable");
            this.CallLuaFunc("OnDisable");
        }

        private void OnDestroy()
        {
            this.CallLuaFunc("OnDestroy");
            //注意这里的UI事件可能会没有被清理
            _val = null;
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            this.CallLuaFunc("OnApplicationPause", pauseStatus);
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            this.CallLuaFunc("OnApplicationFocus", focusStatus);
        }
        
    }
}