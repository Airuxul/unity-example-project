using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace FrameWork.Manager
{
    public class PoolManager : BaseManager
    {
        private readonly Dictionary<string, ObjectPool<GameObject>> _gameObjectPoolDic = new();

        private readonly Transform _poolRoot;
        
        public PoolManager()
        {
            var goRoot = new GameObject("GameObjectPool");
            _poolRoot = goRoot.transform;
            Object.DontDestroyOnLoad(goRoot);
        }

        private ObjectPool<GameObject> CreateGameObjectPool(string path)
        {
            var lastSlashIndex = path.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var name = path[lastSlashIndex..];
            var poolFather = new GameObject(name + "_Pool");
            poolFather.transform.SetParent(_poolRoot.transform);
            return new ObjectPool<GameObject>(CreateFunc, ActionOnGet,ActionOnRelease, ActionOnDestroy);
            
            GameObject CreateFunc()
            {
                var go = AppFacade.AssetLoadManager.Load<GameObject>(path);
                go.name = name;
                return go;  
            }
            
            void ActionOnGet(GameObject go)
            {
                go.transform.SetParent(null);
                go.SetActive(true);
            }
            
            void ActionOnRelease(GameObject go)
            {
                go.SetActive(false);
                go.transform.SetParent(poolFather.transform);
            }

            void ActionOnDestroy(GameObject go)
            {
                Object.Destroy(go);
            }
        }
        
        public GameObject Get(string path)
        {
            if (!_gameObjectPoolDic.TryGetValue(path, out var gameObjectPool))
            {
                gameObjectPool = CreateGameObjectPool(path);
                _gameObjectPoolDic.Add(path, gameObjectPool);
            }
            return gameObjectPool.Get();
        }

        public void Push(string name, GameObject obj) {
            if (!_gameObjectPoolDic.TryGetValue(name, out var gameObjectPool))
            {
                gameObjectPool = CreateGameObjectPool(name);
                _gameObjectPoolDic.Add(name, gameObjectPool);
            }
            gameObjectPool.Release(obj);
        }

        private void Clear()
        {
            foreach (var (_, pool) in _gameObjectPoolDic)
            {
                pool.Dispose();
            }
            _gameObjectPoolDic.Clear();
        }

        public override void Destroy()
        {
            Clear();
            Object.Destroy(_poolRoot);
        }
    }
}