using System.Collections.Generic;
using FrameWork;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class MainPanel : BasePanel
    {
        [SerializeField]
        private Button createButton;
        
        [SerializeField]
        private Button destroyButton;

        private readonly Stack<GameObject> _gameObjectStack = new();
        
        private void Start()
        {
           createButton.onClick.AddListener(CreateGo); 
           destroyButton.onClick.AddListener(DestroyGo);
        }

        private void CreateGo()
        {
            var newGo = AppFacade.PoolManager.Get("Assets/Art/Cube.prefab");
            _gameObjectStack.Push(newGo);
        }

        private void DestroyGo()
        {
            if (_gameObjectStack.Count > 0)
            {
                AppFacade.PoolManager.Push("Assets/Art/Cube.prefab", _gameObjectStack.Pop()); 
            }
        }
    }
}