using System.Collections;
using System.Collections.Generic;
using FrameWork;
using FrameWork.Manager;
using UnityEngine;
using UnityEngine.Events;

//UI层级枚举
public enum E_UI_Layer {
    Bot,
    Mit,
    Top
}


//UI管理器（管理面板）
//管理所有显示的面板
//提供给外部 显示和隐藏
public class UIMgr : BaseManager
{
    public Dictionary<string, BasePanel> panelDic 
        = new Dictionary<string, BasePanel>();

    //这是几个UI面板
    private Transform bot;
    private Transform mid;
    private Transform top;


    public UIMgr() {
        //去找Canvas（做成了预设体在Resources/UI下面）
        GameObject obj = AppFacade.ResMgr.Load<GameObject>("UI/Canvas");
        Transform canvas = obj.transform;
        //创建Canvas，让其过场景的时候不被移除
        GameObject.DontDestroyOnLoad(obj);

        //找到各层
        bot = canvas.Find("bot");
        mid = canvas.Find("mid");
        top = canvas.Find("top");

        //加载EventSystem，有了它，按钮等组件才能响应
        obj = AppFacade.ResMgr.Load<GameObject>("UI/EventSystem");

        //创建Canvas，让其过场景的时候不被移除
        GameObject.DontDestroyOnLoad(obj);
    }
    
    public void ShowPanel<T>(string subFilePath,string panelName,
                        E_UI_Layer layer=E_UI_Layer.Top,
                        UnityAction<T> callback=null) where T:BasePanel {
        ShowPanel(subFilePath+"/"+panelName,layer,callback);
    }
    public void ShowPanel<T>(string panelName,
        E_UI_Layer layer=E_UI_Layer.Top,
        UnityAction<T> callback=null) where T:BasePanel {
        //已经显示了此面板
        if (panelDic.ContainsKey(panelName))
        {
            //处于Active为false的状态
            if (!panelDic[panelName].isActiveAndEnabled)
            {
                panelDic[panelName].gameObject.SetActive(true);
            }
            //调用重写方法，具体内容自己添加
            panelDic[panelName].ShowMe();
            if (callback!=null)
                callback(panelDic[panelName] as T);
            return;
        }
        AppFacade.ResMgr.LoadAsync<GameObject>("UI/"+panelName,(obj)=> {
            //把它作为Canvas的子对象
            //并且设置它的相对位置
            //找到父对象
            if (obj == null)
            {
                Debug.LogError(typeof(T)+"动态生成错误，查看预制体以及脚本路径名是否有误\n" +
                    "脚本路径名:"+panelName);
                return;
            }
            Transform father = bot;
            switch (layer) {
                case E_UI_Layer.Mit:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
            }
            //设置父对象
            obj.transform.SetParent(father);

            //设置相对位置和大小
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预设体身上的脚本（继承自BasePanel）
            T panel = obj.GetComponent<T>();

            //执行外面想要做的事情
            if (callback != null) {
                callback(panel);
            }

            //在字典中添加此面板
            panelDic.Add(panelName, panel);
        });
    }
    //隐藏面板
    public void HidePanel(string panelName) {
        if (panelDic.ContainsKey(panelName)) {
            //调用重写方法，具体内容自己添加
            panelDic[panelName].HideMe();
            panelDic[panelName].gameObject.SetActive(false);
        }
    }
    //删除面板
    public void DestoryPanel(string panelName)
    {
        if(panelDic.ContainsKey(panelName)) {
            //调用重写方法，具体内容自己添加
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }
}
