using System.Collections.Generic;
using FrameWork;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : BasePanel
{

    private Queue<GameObject> testPoolQueue = new Queue<GameObject>();

    private InputField firstNameInputField;
    private InputField secondNameInputField;
    
    void Start()
    {
        firstNameInputField=GetControl<InputField>("FirstNameInputField");
        secondNameInputField = GetControl<InputField>("SecondNameInputField");

        //JsonDataMgr测试
        GetControl<Button>("SaveButton").onClick.AddListener(() =>
        {
            string fn = firstNameInputField.text;
            string sn = secondNameInputField.text;
            People p = new People(fn, sn);
            AppFacade.JsonDataMgr.SaveToJson(p,"test","test.json");
        });
        GetControl<Button>("LoadButton").onClick.AddListener(() =>
        {
            People p= AppFacade.JsonDataMgr.LoadFromJson<People>("test","test.json");
            if (p != null)
            {
                firstNameInputField.text = p.firstName;
                secondNameInputField.text = p.secondName;
            }
           
        });
        
        //PoolMgr测试
        GetControl<Button>("GetButton").onClick.AddListener(() =>
        {
            testPoolQueue.Enqueue(AppFacade.PoolMgr.GetObj("PoolTest"));
        });
        GetControl<Button>("PushButton").onClick.AddListener(() =>
        {
            if (testPoolQueue.Count != 0)
            {
                AppFacade.PoolMgr.PushObj("PoolTest",testPoolQueue.Dequeue());
            }
        });
        
        //MusicMgr测试
        GetControl<Button>("PlaySoundButton").onClick.AddListener(() => 
            {
                AppFacade.MusicMgr.PlaySound("1",false);
            }
        );
    }
}
