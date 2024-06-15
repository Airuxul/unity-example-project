using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FrameWork.Manager.UIManager
{
    public class BasePanel : MonoBehaviour
    {
        private readonly Dictionary<string, List<UIBehaviour>> _controlDic = new();

        protected virtual void Awake()
        {
            FindChildControl<Button>();
            FindChildControl<Image>();
            FindChildControl<Text>();
            FindChildControl<Toggle>();
            FindChildControl<ScrollRect>();
            FindChildControl<Slider>();
            FindChildControl<InputField>();
        }

        //得到对应名字的对应控件脚本
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (_controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < _controlDic[controlName].Count; i++)
                {
                    //对应字典的值（是个集合）中，符合要求的类型的
                    //则返回出去，这样外部就
                    if (_controlDic[controlName][i] is T)
                    {
                        return _controlDic[controlName][i] as T;
                    }
                }
            }

            return null;
        }

        //找到相对应的UI控件并记录到字典中
        private void FindChildControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>();
            for (int i = 0; i < controls.Length; i++)
            {
                var objName = controls[i].gameObject.name;
                if (_controlDic.TryGetValue(objName, value: out var value))
                {
                    value.Add(controls[i]);
                }
                else
                {
                    _controlDic.Add(objName, new List<UIBehaviour> { controls[i] });
                }
            }
        }

        //让子类重写（覆盖）此方法，来实现UI的隐藏与出现
        public virtual void ShowMe()
        {
        }

        public virtual void HideMe()
        {
        }
    }
}