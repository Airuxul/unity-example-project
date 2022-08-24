using System;
using System.Collections.Generic;
using FrameWork.Manager;
using Utility;

namespace FrameWork
{
    public class AppFacade:Singleton<AppFacade>
    {
        #region Managers
        //可能需要重构一次
        public static MonoMgr MonoMgr { get; private set; }
        
        public static JsonDataMgr JsonDataMgr { get; private set; }
        
        public static EventMgr EventMgr { get; private set; }
        
        public static ResMgr ResMgr { get; private set; }
        
        public static DelayMgr DelayMgr { get; private set; }
        
        public static SceneMgr ScenesMgr { get; private set; }
        
        public static UIMgr UIMgr { get; private set; }
        
        public static PoolMgr PoolMgr { get; private set; }

        public static MusicMgr MusicMgr { get; private set; }
        
        public static InputMgr InputMgr { get; private set; }

        private List<IManager> mgrs;
        #endregion
        
        public void SetupManager()
        {
            MonoMgr = AddManager<MonoMgr>();
            JsonDataMgr = AddManager<JsonDataMgr>();
            EventMgr = AddManager<EventMgr>();
            
            //依赖MonoMgr
            ResMgr = AddManager<ResMgr>();
            DelayMgr = AddManager<DelayMgr>();
            
            //依赖MonoMgr和EventMgr
            ScenesMgr = AddManager<SceneMgr>();

            //依赖ResMgr
            UIMgr = AddManager<UIMgr>();
            PoolMgr = AddManager<PoolMgr>();
            
            //依赖ResMgr和MonoMgr
            MusicMgr = AddManager<MusicMgr>();
            InputMgr = AddManager<InputMgr>();
        }

        public void InitAllManager()
        {
            foreach (var mgr in mgrs)
            {
                mgr.Init();
            }
        }

        public void DestroyAllManager()
        {
            foreach (var mgr in mgrs)
            {
                mgr.Destroy();
            }
        }

        private T AddManager<T>() where T : IManager
        {
            T mgr = Activator.CreateInstance<T>();
            mgrs.Add(mgr);
            return mgr;
        }
    }
}