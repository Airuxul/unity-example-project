using FrameWork;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class StartPanel : BasePanel
    {
        [SerializeField]
        private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private static void StartGame()
        {
            Debug.Log("Start Game");
            AppFacade.ScenesManager.LoadSceneAsync("Level1", () =>
            {
                AppFacade.UIManager.ShowPanel<MainPanel>();
                AppFacade.UIManager.DestroyAllPanel();
            });
        }
    }
}