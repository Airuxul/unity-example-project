using System;
using LitJson;
using UnityEngine;

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
    void Start()
    {
       UIManager.GetInstance().ShowPanel<TestPanel>("TestPanel");
    }
    
}
