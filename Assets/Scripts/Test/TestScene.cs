using System;
using FrameWork;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 测试类
/// </summary>
class People
{
    public string firstName;
    public string secondName;
    public People(string firstName, string secondName)
    {
        this.firstName = firstName;
        this.secondName = secondName;
    }
    public People() {}
}
public class TestScene : MonoBehaviour
{
    private UnityAction ua1;
    private UnityAction ua2;
    public GameObject go;
    private GameObject _go;
    private void Awake()
    {
        AppFacade.Instance.SetupManager();
        AppFacade.Instance.InitAllManager();
    }

    void Start()
    {
        AppFacade.UIMgr.ShowPanel<TestPanel>("Test","TestPanel");
        AppFacade.LuaMgr.Lua.DoFile("Main.lua");
        AppFacade.LuaMgr.Lua.DoFile("Adapter.lua");
        _go = Instantiate(go);
    }
}
