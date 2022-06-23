using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RegistData
{
    private string username;
    private string password;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
}


public class RegistPanel : UIBasePanel
{
    private RegistData registData;

    protected override void Awake()
    {
        base.Awake();
        registData = new();
        BindEvent();
    }

    private void BindEvent()
    {
        GetWidget<InputField>("Username_N").onEndEdit.AddListener(username => EditUsername(username));
        GetWidget<InputField>("Password_N").onEndEdit.AddListener(password => EditPassword(password));
        GetWidget<Button>("Submit_Button_N").onClick.AddListener(() => Submit());
        GetWidget<Button>("Cancel_Button_N").onClick.AddListener(() => Cancel());
    }

    private void EditUsername(string username)
    {
        Debug.Log($"注册的Username: {username}");
        registData.Username = username;
    }
    private void EditPassword(string password)
    {
        Debug.Log($"注册的Password: {password}");
        registData.Username = password;
    }
    private void Submit()
    {
        Debug.Log("提交注册信息");
        BackToLogin();
    }
    private void Cancel()
    {
        Debug.Log("取消了注册");
        BackToLogin();
    }
    private void BackToLogin()
    {
        UIManager.Instance.GetWidget("LoginPanel", "Root_N").gameObject.SetActive(true);
        Destroy(gameObject);
    }
}