using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

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
    void Start()
    {
        UIManager.GetInstance().ShowPanel<TestPanel>("Test","TestPanel");
    }

}
