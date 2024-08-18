using System;
using System.Collections.Generic;
using FrameWork.Manager;

namespace FrameWork
{
    public static class AppFacade
    {
        #region Managers
        public static MonoManager MonoManager { get; private set; }
        
        public static EventManager EventManager { get; private set; }
        
        public static AssetLoadManager AssetLoadManager { get; private set; }
        
        public static SceneManager ScenesManager { get; private set; }
        
        public static UIManager UIManager { get; private set; }
        
        public static PoolManager PoolManager { get; private set; }
        
        public static InputManager InputManager { get; private set; }

        private static readonly List<IManager> Managers = new();
        #endregion
        
        public static void SetupManager()
        {
            MonoManager = AddManager<MonoManager>();
            EventManager = AddManager<EventManager>();
            AssetLoadManager = AddManager<AssetLoadManager>();
            ScenesManager = AddManager<SceneManager>();
            UIManager = AddManager<UIManager>();
            PoolManager = AddManager<PoolManager>();
            InputManager = AddManager<InputManager>();
        }

        public static void InitAllManager()
        {
            foreach (var mgr in Managers)
            {
                mgr.Init();
            }
        }

        public static void DestroyAllManager()
        {
            foreach (var mgr in Managers)
            {
                mgr.Destroy();
            }
        }

        private static T AddManager<T>() where T : IManager
        {
            T mgr = Activator.CreateInstance<T>();
            Managers.Add(mgr);
            return mgr;
        }
    }
}