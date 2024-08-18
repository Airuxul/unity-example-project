using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    public class MonoManager : BaseManager
    {
        private MonoController _controller;

        public MonoController MonoController => _controller;

        public MonoManager()
        {
            var obj = new GameObject("MonoController");
            _controller = obj.AddComponent<MonoController>();
        }

        public void AddUpdateListener(UnityAction func)
        {
            _controller.AddUpdateListener(func);
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            _controller.RemoveUpdateListener(func);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return _controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return _controller.StartCoroutine(methodName);
        }

        public void StopCoroutine(Coroutine e)
        {
            _controller.StopCoroutine(e);
        }

        public void StopAllCoroutines()
        {
            _controller.StopAllCoroutines();
        }

        public override void Destroy()
        {
            Object.Destroy(_controller);
            _controller = null;
        }
    }
}