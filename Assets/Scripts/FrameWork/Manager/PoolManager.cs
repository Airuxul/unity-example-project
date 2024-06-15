using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Manager
{
    public interface IObjectPool<T> where T : Object
    {
        void PushObj(T obj);
        T GetObj();
        bool DoHava();
    }
    
    public abstract class ObjectPool<T> : IObjectPool<T> where T : Object
    {
        protected Queue<T> objQueuePool = new();

        public virtual void PushObj(T obj)
        {
            objQueuePool.Enqueue(obj);
        }
        
        public virtual T GetObj() { return default;}
        
        public virtual bool DoHava()
        {
            return objQueuePool.Count >0;
        }

        public virtual void Clear()
        {
            objQueuePool.Clear();
            objQueuePool = null;
        }
    }

    public class GameObjectPool : ObjectPool<GameObject>
    {
        private readonly GameObject _fatherGameObject;
        
        public GameObjectPool(GameObject poolRoot,string poolRootName)
        {
            _fatherGameObject = new GameObject
            {
                name = poolRootName+"s-Pool"
            };
            _fatherGameObject.transform.SetParent(poolRoot.transform);
        }
        
        public override void PushObj(GameObject obj)
        {
            base.PushObj(obj);
            obj.transform.SetParent(_fatherGameObject.transform);
            obj.SetActive(false);
        }
        
        public override GameObject GetObj()
        {
            var gameObject = objQueuePool.Dequeue();
            gameObject.transform.SetParent(null);
            gameObject.SetActive(true);
            return gameObject;
        }

        public override void Clear()
        {
            foreach (var go in objQueuePool)
            {
                Object.Destroy(go);
            }
            Object.Destroy(_fatherGameObject);
            base.Clear();
        }
    }


    public class PoolManager : BaseManager
    {
        private readonly Dictionary<string,GameObjectPool> _gameObjectPoolDic = new();

        private GameObject _poolFather;
        
        public GameObject GetObj(string name)
        {
            if (_gameObjectPoolDic.TryGetValue(name, out var gameObjectPool) && gameObjectPool.DoHava())
            {
                return _gameObjectPoolDic[name].GetObj();
            }
            
            var gameObject=AppFacade.ResManager.Load<GameObject>(name);
            gameObject.name = name;
            return gameObject;
        }
        
        public void PushObj(string name,GameObject obj) {
            if (_poolFather == null)
            {
                _poolFather = new GameObject("GameObjectPool");
            }
            //里面有记录这个键
            if (_gameObjectPoolDic.TryGetValue(name, out var gameObj))
            {
                gameObj.PushObj(obj);
            }
            else 
            {
                _gameObjectPoolDic.Add(name, new GameObjectPool(_poolFather, name));
                _gameObjectPoolDic[name].PushObj(obj);
            }
        }

        public void Clear()
        {
            foreach (var pool in _gameObjectPoolDic)
            {
                pool.Value.Clear();
            }
            _gameObjectPoolDic.Clear();
        }

        public override void Destroy()
        {
            Clear();
            Object.Destroy(_poolFather);
        }
    }
}