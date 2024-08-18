using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    //UI层级枚举
    public enum UILayer
    {
        Base,
        Pop,
        Notice,
        Top
    }

    public class UIManager : BaseManager
    {
        private readonly Dictionary<string, BasePanel> _panelDic = new();

        private readonly Transform _baseParent;
        private readonly Transform _popParent;
        private readonly Transform _noticeParent;
        private readonly Transform _topParent;

        public UIManager()
        {
            //去找Canvas（做成了预设体在Resources/UI下面）
            var obj = AppFacade.AssetLoadManager.Load<GameObject>(UIConst.UICanvas);
            var canvas = obj.transform;
            //创建Canvas，让其过场景的时候不被移除
            Object.DontDestroyOnLoad(obj);
            
            _baseParent = canvas.Find("base");
            _popParent = canvas.Find("pop");
            _noticeParent = canvas.Find("notice");
            _topParent = canvas.Find("top");

            //加载EventSystem，有了它，按钮等组件才能响应
            obj = AppFacade.AssetLoadManager.Load<GameObject>(UIConst.UIEventSystem);

            //创建Canvas，让其过场景的时候不被移除
            Object.DontDestroyOnLoad(obj);
        }

        public void ShowPanel<T>(UnityAction<T> callback = null) where T : BasePanel
        {
            var panelConfig = UIConfig.GetPanelConfig(typeof(T));
            var prefabPath = panelConfig.PrefabPath;
            var layer = panelConfig.UILayer;
            //已经显示了此面板
            if (_panelDic.TryGetValue(prefabPath, out var panel))
            {
                //处于Active为false的状态
                if (!panel.Visible)
                {
                    panel.ShowMe();
                }
                if (layer == UILayer.Pop)
                {
                    panel.transform.SetAsLastSibling();
                }
                callback?.Invoke(panel as T);
                return;
            }

            AppFacade.AssetLoadManager.LoadAsync<GameObject>(prefabPath, obj =>
            {
                if (obj == null)
                {
                    Debug.LogError(typeof(T) + "动态生成错误，查看预制体以及脚本路径名是否有误\n" +
                                   "脚本路径名:" + prefabPath);
                    return;
                }

                Transform father = GetUIParent(layer);

                obj.transform.SetParent(father);
                //设置相对位置和大小
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                ((RectTransform)obj.transform).offsetMax = Vector2.zero;
                ((RectTransform)obj.transform).offsetMin = Vector2.zero;

                //得到预设体身上的脚本（继承自BasePanel）
                var panelComp = obj.GetComponent<T>();
                _panelDic.Add(prefabPath, panelComp);
                //执行外面想要做的事情
                callback?.Invoke(panelComp);
            });
        }

        public Transform GetUIParent(UILayer uiLayer)
        {
            Transform uiParent = null;
            switch (uiLayer)
            {
                case UILayer.Base:
                    uiParent = _baseParent;
                    break;
                case UILayer.Pop:
                    uiParent = _popParent;
                    break;
                case UILayer.Notice:
                    uiParent = _noticeParent;
                    break;
                case UILayer.Top:
                    uiParent = _topParent;
                    break;
            }

            return uiParent;
        }

        //显示面板
        public void ShowPanel(string panelName)
        {
            if (_panelDic.TryGetValue(panelName, out var panel))
            {
                panel.ShowMe();
            }
        }

        //隐藏面板
        public void HidePanel(string panelName)
        {
            if (_panelDic.TryGetValue(panelName, out var panel))
            {
                panel.HideMe();
            }
        }

        //删除面板
        public void DestroyPanel(string panelName)
        {
            if (!_panelDic.TryGetValue(panelName, out var panel)) return;
            panel.HideMe();
            Object.Destroy(panel.gameObject);
            _panelDic.Remove(panelName);
        }

        public void DestroyAllPanel()
        {
            foreach (var panelName in _panelDic.Keys)
            {
                 if (!_panelDic.TryGetValue(panelName, out var panel)) 
                    continue;
                 panel.HideMe(); 
                 Object.Destroy(panel.gameObject);   
            }
            _panelDic.Clear();
        }
    }
}