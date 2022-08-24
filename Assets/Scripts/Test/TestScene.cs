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

    private void Awake()
    {
        AppFacade.Instance.SetupManager();
    }

    void Start()
    {
        AppFacade.UIMgr.ShowPanel<TestPanel>("Test","TestPanel");
    }

}
