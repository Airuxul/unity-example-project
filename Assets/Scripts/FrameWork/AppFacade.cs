using System;
using System.Collections.Generic;
using FrameWork.Manager;
using Utility;

namespace FrameWork
{
    public class AppFacade:Singleton<AppFacade>
    {
        #region Managers
        public static MonoManager MonoManager { get; private set; }
        
        public static EventManager EventManager { get; private set; }
        
        public static ResManager ResManager { get; private set; }
        
        public static SceneManager ScenesManager { get; private set; }
        
        public static UIMgr UIMgr { get; private set; }
        
        public static PoolManager PoolManager { get; private set; }
        
        public static InputManager InputManager { get; private set; }

        private readonly List<IManager> _mgrs = new();
        #endregion
        
        public void SetupManager()
        {
            MonoManager = AddManager<MonoManager>();
            EventManager = AddManager<EventManager>();
            ResManager = AddManager<ResManager>();
            ScenesManager = AddManager<SceneManager>();
            UIMgr = AddManager<UIMgr>();
            PoolManager = AddManager<PoolManager>();
            InputManager = AddManager<InputManager>();
        }

        public void InitAllManager()
        {
            foreach (var mgr in _mgrs)
            {
                mgr.Init();
            }
        }

        public void DestroyAllManager()
        {
            foreach (var mgr in _mgrs)
            {
                mgr.Destroy();
            }
        }

        private T AddManager<T>() where T : IManager
        {
            T mgr = Activator.CreateInstance<T>();
            _mgrs.Add(mgr);
            return mgr;
        }
    }
}