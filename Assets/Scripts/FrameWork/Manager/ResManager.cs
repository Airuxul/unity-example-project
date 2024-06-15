using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork.Manager
{
    //资源加载模块
    public class ResManager : BaseManager
    {
        //同步加载资源
        public T Load<T>(string name) where T : Object
        {
#if UNITY_EDITOR
            T res = AssetDatabase.LoadAssetAtPath<T>(name);
#else
           // TODO:AssetBundle加载
#endif
            // TODO:后续可能需要对GO进行处理
            return res;
        }

        //异步加载资源 
        public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            //开启异步加载的协程
            AppFacade.MonoManager.StartCoroutine(ReallyLoadAsync(name, callback));
        }

        private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
#if UNITY_EDITOR
            ResourceRequest r = Resources.LoadAsync<T>(name);
#else 
            // TODO:AssetBundle加载
#endif
            // TODO:后续可能需要对GO进行额外处理
            yield return r;
            callback(r.asset as T);
        }
    }
}