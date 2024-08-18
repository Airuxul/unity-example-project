using FrameWork;
using UI.Panel;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    void Awake()
    {
        AppFacade.SetupManager();
        AppFacade.UIManager.ShowPanel<StartPanel>(startPanel =>
        {
            
        });
        // 使用job system创建100个物体
    }
}
