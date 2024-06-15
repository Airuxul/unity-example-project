using FrameWork;
using UI.Panel;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    void Awake()
    {
        AppFacade.SetupManager();
        AppFacade.UIManager.ShowPanel<TestPanel>(testPanel =>
        {
            testPanel.Test();
        });
    }
}
