using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : BasePanel
{
    
    private InputField firstNameInputField;
    private InputField secondNameInputField;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("TestPanelAwake");
    }
    void Start()
    {
        Debug.Log("TestPanelStartFunc");
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
    }
}
