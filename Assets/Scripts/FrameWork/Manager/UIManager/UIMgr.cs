using System.Collections.Generic;
using FrameWork;
using FrameWork.Manager;
using FrameWork.Manager.UIManager;
using UnityEngine;
using UnityEngine.Events;

//UI层级枚举
public enum E_UI_Layer
{
    Bot,
    Mit,
    Top
}

public class UIMgr : BaseManager
{
    private readonly Dictionary<string, BasePanel> _panelDic = new();
    public Dictionary<string, BasePanel> PanelDic => _panelDic;

    //这是几个UI面板
    private readonly Transform _bot;
    private readonly Transform _mid;
    private readonly Transform _top;


    public UIMgr()
    {
        //去找Canvas（做成了预设体在Resources/UI下面）
        var obj = AppFacade.ResManager.Load<GameObject>("UI/Canvas");
        var canvas = obj.transform;
        //创建Canvas，让其过场景的时候不被移除
        Object.DontDestroyOnLoad(obj);

        //找到各层
        _bot = canvas.Find("bot");
        _mid = canvas.Find("mid");
        _top = canvas.Find("top");

        //加载EventSystem，有了它，按钮等组件才能响应
        obj = AppFacade.ResManager.Load<GameObject>("UI/EventSystem");

        //创建Canvas，让其过场景的时候不被移除
        Object.DontDestroyOnLoad(obj);
    }

    public void ShowPanel<T>(
        string subFilePath, 
        string panelName,
        E_UI_Layer layer = E_UI_Layer.Top,
        UnityAction<T> callback = null
        ) where T : BasePanel
    {
        ShowPanel(subFilePath + "/" + panelName, layer, callback);
    }

    public void ShowPanel<T>(
        string panelName,
        E_UI_Layer layer = E_UI_Layer.Top,
        UnityAction<T> callback = null
        ) where T : BasePanel
    {
        //已经显示了此面板
        if (_panelDic.ContainsKey(panelName))
        {
            //处于Active为false的状态
            if (!_panelDic[panelName].isActiveAndEnabled)
            {
                _panelDic[panelName].gameObject.SetActive(true);
            }

            //调用重写方法，具体内容自己添加
            _panelDic[panelName].ShowMe();
            if (callback != null)
                callback(_panelDic[panelName] as T);
            return;
        }

        AppFacade.ResManager.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //把它作为Canvas的子对象
            //并且设置它的相对位置
            //找到父对象
            if (obj == null)
            {
                Debug.LogError(typeof(T) + "动态生成错误，查看预制体以及脚本路径名是否有误\n" +
                               "脚本路径名:" + panelName);
                return;
            }

            Transform father = _bot;
            switch (layer)
            {
                case E_UI_Layer.Mit:
                    father = _mid;
                    break;
                case E_UI_Layer.Top:
                    father = _top;
                    break;
            }

            //设置父对象
            obj.transform.SetParent(father);

            //设置相对位置和大小
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            ((RectTransform)obj.transform).offsetMax = Vector2.zero;
            ((RectTransform)obj.transform).offsetMin = Vector2.zero;

            //得到预设体身上的脚本（继承自BasePanel）
            var panel = obj.GetComponent<T>();

            //执行外面想要做的事情
            if (callback != null)
            {
                callback(panel);
            }

            //在字典中添加此面板
            _panelDic.Add(panelName, panel);
        });
    }

    //隐藏面板
    public void HidePanel(string panelName)
    {
        if (_panelDic.ContainsKey(panelName))
        {
            //调用重写方法，具体内容自己添加
            _panelDic[panelName].HideMe();
            _panelDic[panelName].gameObject.SetActive(false);
        }
    }

    //删除面板
    public void DestoryPanel(string panelName)
    {
        if (_panelDic.ContainsKey(panelName))
        {
            //调用重写方法，具体内容自己添加
            _panelDic[panelName].HideMe();
            GameObject.Destroy(_panelDic[panelName].gameObject);
            _panelDic.Remove(panelName);
        }
    }
}