using System;
using System.Collections.Generic;
using UI.Panel;
using FrameWork.Manager.UIManager;

namespace UI
{
    public class PanelConfig
    {
        public readonly Type panelType;
        public readonly string prefabPath;
        public readonly UILayer uiLayer;
        public PanelConfig(Type panelType, string prefabPath, UILayer uiLayer)
        {
            this.panelType = panelType;
            this.prefabPath = UIConst.UIPanelFolder + prefabPath;
            this.uiLayer = uiLayer;
        }
    }
    
    public static class UIConfig
    {
        private static readonly Dictionary<Type, PanelConfig> PanelConfigDict;

        static UIConfig()
        {
            PanelConfigDict = new Dictionary<Type, PanelConfig>();
            AddPanelConfig(new PanelConfig(
                typeof(TestPanel),
                "TestPanel.prefab",
                UILayer.Pop
                )
            );
        }

        private static void AddPanelConfig(PanelConfig panelConfig)
        {
            PanelConfigDict.Add(panelConfig.panelType, panelConfig);
        }
        
        public static PanelConfig GetPanelConfig(Type panelType)
        {
            if (PanelConfigDict.TryGetValue(panelType, out var panelConfig))
            {
                return panelConfig;
            }
            throw new Exception(panelType +"PanelConfig not found!");
        }
    }
}