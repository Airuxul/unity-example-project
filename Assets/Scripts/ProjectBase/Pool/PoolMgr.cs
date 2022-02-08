using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;


public interface IObjectPool<T> where T : Object
{
    public void PushObj(T obj);
    public T GetObj();
    public bool doHava();
}
public abstract class ObjectPool<T> : IObjectPool<T> where T : Object
{
    protected readonly Queue<T> objQueuePool;

    protected ObjectPool()
    {
        objQueuePool = new Queue<T>();
    }
    public virtual void PushObj(T obj)
    {
        objQueuePool.Enqueue(obj);
    }
    public virtual T GetObj() { return default;}
    public virtual bool doHava()
    {
        return objQueuePool.Count >0 ? true : false;
    }
}

public class GameObjectPool : ObjectPool<GameObject>
{
    private GameObject fatherGameObject;
    public GameObjectPool(GameObject poolRoot,string poolRootName)
    {
        fatherGameObject = new GameObject
        {
            name = poolRootName+"s-Pool"
        };
        fatherGameObject.transform.SetParent(poolRoot.transform);
    }
    public override void PushObj(GameObject obj)
    {
        base.PushObj(obj);
        obj.transform.SetParent(fatherGameObject.transform);
        obj.SetActive(false);
    }
    public override GameObject GetObj()
    {
        GameObject gameObject;
        gameObject = objQueuePool.Dequeue();
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        return gameObject;
    }
}


public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string,GameObjectPool> GameObjectPoolDic
        =new Dictionary<string, GameObjectPool>();

    private GameObject poolFather;
    //取得游戏物体
    public GameObject GetObj(string name) {
        if (GameObjectPoolDic.ContainsKey(name) && GameObjectPoolDic[name].doHava())
        {
            return GameObjectPoolDic[name].GetObj();
        }
        else{
            //缓存池中没有该物体，我们去目录中加载
            //外面传一个预设体的路径和名字，我内部就去加载它
            GameObject gameObject=ResMgr.GetInstance().Load<GameObject>(name);
            gameObject.name = name;
            return gameObject;
        }
    }

    //外界返还游戏物体
    public void PushObj(string name,GameObject obj) {
        if (poolFather == null)
        {
            poolFather = new GameObject("GameObjectPool");
        }
        //里面有记录这个键
        if (GameObjectPoolDic.ContainsKey(name))
        {
            GameObjectPoolDic[name].PushObj(obj);
        }
        //未曾记录这个键
        else {
            GameObjectPoolDic.Add(name, new GameObjectPool(poolFather,name));
            GameObjectPoolDic[name].PushObj(obj);
        }
    }
    
    //清空缓存池的方法
    //主要用在场景切换时
    public void Clear() {
        GameObjectPoolDic.Clear();
        poolFather = null;
    }
}
