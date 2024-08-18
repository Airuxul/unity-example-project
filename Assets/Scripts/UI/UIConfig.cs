using System;
using System.Collections.Generic;
using UI.Panel;
using FrameWork.Manager;

namespace UI
{
    public class PanelConfig
    {
        public readonly Type PanelType;
        public readonly string PrefabPath;
        public readonly UILayer UILayer;
        public PanelConfig(Type panelType, string prefabPath, UILayer uiLayer)
        {
            PanelType = panelType;
            PrefabPath = UIConst.UIPanelFolder + prefabPath;
            UILayer = uiLayer;
        }
    }
    
    public static class UIConfig
    {
        private static readonly Dictionary<Type, PanelConfig> PanelConfigDict;

        static UIConfig()
        {
            PanelConfigDict = new Dictionary<Type, PanelConfig>();
            AddPanelConfig(new PanelConfig(
                typeof(StartPanel),
                "StartPanel.prefab",
                UILayer.Base
                )
            );
            AddPanelConfig(new PanelConfig(
                typeof(MainPanel),
                "MainPanel.prefab",
                UILayer.Pop
                )
            );
        }

        private static void AddPanelConfig(PanelConfig panelConfig)
        {
            PanelConfigDict.Add(panelConfig.PanelType, panelConfig);
        }
        
        public static PanelConfig GetPanelConfig(Type panelType)
        {
            if (PanelConfigDict.TryGetValue(panelType, out var panelConfig))
            {
                return panelConfig;
            }
            throw new Exception(panelType + "PanelConfig not found!");
        }
    }
}