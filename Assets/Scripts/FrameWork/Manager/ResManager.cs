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
        public T Load<T>(string path) where T : Object
        {
#if UNITY_EDITOR
            T res = AssetDatabase.LoadAssetAtPath<T>(path);
#else
           // TODO:AssetBundle加载
#endif
            // TODO:后续可能需要对GO进行处理
            return res is GameObject ? Object.Instantiate(res) : res;
        }

        //异步加载资源 
        public void LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            //开启异步加载的协程
            AppFacade.MonoManager.StartCoroutine(ReallyLoadAsync(path, callback));
        }

        private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
#if UNITY_EDITOR
            T res = AssetDatabase.LoadAssetAtPath<T>(path);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(res, out string guid, out long localId);
            var op = AssetDatabase.LoadObjectAsync(path, localId);
            while (!op.isDone)
            {
                yield return null;
            }

            if (op.LoadedObject is GameObject)
            {
                callback(Object.Instantiate(op.LoadedObject) as T);
            }
            else
            {
                callback(op.LoadedObject as T);
            }
#else 
            // TODO:AssetBundle加载
#endif
        }
    }
}