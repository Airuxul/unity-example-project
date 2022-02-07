using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : BasePanel
{

    private Queue<GameObject> testPoolQueue = new Queue<GameObject>();

    private InputField firstNameInputField;
    private InputField secondNameInputField;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        firstNameInputField=GetControl<InputField>("FirstNameInputField");
        secondNameInputField = GetControl<InputField>("SecondNameInputField");
        GetControl<Button>("SaveButton").onClick.AddListener(() =>
        {
            string fn = firstNameInputField.text;
            string sn = secondNameInputField.text;
            People p = new People(fn, sn);
            JsonDataMgr.GetInstance().SaveToJson(p,"test","test.json");
        });
        
        GetControl<Button>("LoadButton").onClick.AddListener(() =>
        {
            People p= JsonDataMgr.GetInstance().LoadFromJson<People>("test","test.json");
            if (p != null)
            {
                firstNameInputField.text = p.firstName;
                secondNameInputField.text = p.secondName;
            }
           
        });
        GetControl<Button>("GetButton").onClick.AddListener(() =>
        {
            testPoolQueue.Enqueue(PoolMgr.GetInstance().GetObj("PoolTest"));
        });
        GetControl<Button>("PushButton").onClick.AddListener(() =>
        {
            if (testPoolQueue.Count != 0)
            {
                PoolMgr.GetInstance().PushObj("PoolTest",testPoolQueue.Dequeue());
            }
        });
    }
}
