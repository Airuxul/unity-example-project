using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    public class MonoMgr : BaseManager
    {
        private MonoController controller;
        public MonoMgr() {
            //新建一个物体
            GameObject obj = new GameObject("MonoController");
            //给物体添加组件
            controller = obj.AddComponent<MonoController>();
        }
        public void AddUpdateListener(UnityAction func)
        {
            controller.AddUpdateListener(func);
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            controller.RemoveUpdateListener(func);
        }
        public Coroutine StartCoroutine(IEnumerator routine) {
            return controller.StartCoroutine(routine);
        }
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) {
            return controller.StartCoroutine(methodName,value);
        }
        public Coroutine StartCoroutine(string methodName) {
            return controller.StartCoroutine(methodName);
        }
        public void StopCoroutine(Coroutine e)
        {
            controller.StopCoroutine(e);
        }

        public void StopAllCoroutines()
        {
            controller.StopAllCoroutines();
        }

        public override void Destroy()
        {
            GameObject.Destroy(controller);
            controller = null;
        }
    }
}
