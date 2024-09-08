using FrameWork;
using FrameWork.Manager;
using UI;
using UI.Panel;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    void Awake()
    {
        InitUIConfig();
        AppFacade.SetupManager();
        AppFacade.UIManager.ShowPanel<StartPanel>(startPanel =>
        {
            
        });
    }

    private void InitUIConfig()
    {
        UIConfig.AddPanelConfig(new PanelConfig(
                typeof(StartPanel),
                "StartPanel.prefab",
                UILayer.Base
            )
        );
        UIConfig.AddPanelConfig(new PanelConfig(
                typeof(MainPanel),
                "MainPanel.prefab",
                UILayer.Base
            )
        );
    }
}
