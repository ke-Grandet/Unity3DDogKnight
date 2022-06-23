using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LoginData
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


public class LoginPanel : UIBasePanel
{
    private LoginData loginData;
    protected override void Awake()
    {
        base.Awake();
        loginData = new();
        BindEvent();
    }

    private void BindEvent()
    {
        GetWidget<InputField>("Username_N").onEndEdit.AddListener(username => EditUsername(username));
        GetWidget<InputField>("Password_N").onEndEdit.AddListener(password => EditPassword(password));
        GetWidget<Button>("Login_Button_N").onClick.AddListener(() => LoginClick());
        GetWidget<Button>("Regist_Button_N").onClick.AddListener(() => RegistClick());
    }


    // �����ǰ󶨵��ӿؼ��ϵľ����¼�

    private void EditUsername(string username)
    {
        Debug.Log($"��¼��Username === {username}");
        loginData.Username = username;
    }
    private void EditPassword(string password)
    {
        Debug.Log($"��¼��Password === {password}");
        loginData.Username = password;
    }
    private void LoginClick()
    {
        Debug.Log("����˵�¼");
        // ��������ֵ����
        Transform healthPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.healthPanel);
        healthPanel.gameObject.AddComponent<HealthPanel>();
        // ����ҡ�˽���
        Transform joystickPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.joystickPanel);
        joystickPanel.gameObject.AddComponent<JoystickPanel>();
        // ���ؼ��ܽ���
        Transform skillPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.skillPanel);
        skillPanel.gameObject.AddComponent<SkillPanel>();
        // ���ٵ�¼����
        Destroy(gameObject);
    }
    private void RegistClick()
    {
        Debug.Log("�����ע��");
        Transform registPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.registPanel);
        registPanel.gameObject.AddComponent<RegistPanel>();
        HideSelf();
    }
}