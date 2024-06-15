using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager.UIManager
{
    //UI层级枚举
    public enum UILayer
    {
        Pop,
        Notice,
    }

    public class UIManager : BaseManager
    {
        private readonly Dictionary<string, BasePanel> _panelDic = new();

        private readonly Transform _popParent;
        private readonly Transform _noticeParent;

        public UIManager()
        {
            //去找Canvas（做成了预设体在Resources/UI下面）
            var obj = AppFacade.ResManager.Load<GameObject>("Assets/Art/UI/Canvas.prefab");
            var canvas = obj.transform;
            //创建Canvas，让其过场景的时候不被移除
            Object.DontDestroyOnLoad(obj);

            _popParent = canvas.Find("pop");
            _noticeParent = canvas.Find("notice");

            //加载EventSystem，有了它，按钮等组件才能响应
            obj = AppFacade.ResManager.Load<GameObject>("Assets/Art/UI/EventSystem.prefab");

            //创建Canvas，让其过场景的时候不被移除
            Object.DontDestroyOnLoad(obj);
        }

        public void ShowPanel<T>(UnityAction<T> callback = null) where T : BasePanel
        {
            var panelConfig = UIConfig.GetPanelConfig(typeof(T));
            string panelName = panelConfig.prefabPath;
            UILayer layer = panelConfig.uiLayer;
            //已经显示了此面板
            if (_panelDic.TryGetValue(panelName, out var panel))
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

            AppFacade.ResManager.LoadAsync<GameObject>(panelName, obj =>
            {
                if (obj == null)
                {
                    Debug.LogError(typeof(T) + "动态生成错误，查看预制体以及脚本路径名是否有误\n" +
                                   "脚本路径名:" + panelName);
                    return;
                }

                Transform father = null;
                switch (layer)
                {
                    case UILayer.Notice:
                        father = _popParent;
                        break;
                    case UILayer.Pop:
                        father = _noticeParent;
                        break;
                }

                obj.transform.SetParent(father);
                //设置相对位置和大小
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                ((RectTransform)obj.transform).offsetMax = Vector2.zero;
                ((RectTransform)obj.transform).offsetMin = Vector2.zero;

                //得到预设体身上的脚本（继承自BasePanel）
                var panelComp = obj.GetComponent<T>();
                panelComp.config = panelConfig;
                _panelDic.Add(panelName, panelComp);
                //执行外面想要做的事情
                callback?.Invoke(panelComp);
            });
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
        public void DestoryPanel(string panelName)
        {
            if (_panelDic.TryGetValue(panelName, out var panel))
            {
                panel.HideMe();
                Object.Destroy(panel.gameObject);
                _panelDic.Remove(panelName);
            }
        }
    }
}