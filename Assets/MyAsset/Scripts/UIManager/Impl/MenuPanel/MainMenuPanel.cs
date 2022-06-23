using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainMenuPanel : UIBasePanel
{
    private Image startButton;
    private Image quitButton;

    protected override void Awake()
    {
        base.Awake();
        InitWidget();
        BindListener();
    }

    private void InitWidget()
    {
        startButton = GetWidget<Image>("Start_N");
        quitButton = GetWidget<Image>("Quit_N");
    }

    private void BindListener()
    {
        // 绑定Start按钮事件
        AddEventToWidget(startButton, EventTriggerType.PointerClick, baseEventData =>
        {
            // 开启进攻
            //PlayerManager.Instance.Holy.StartDefense();
            // 加载生命值界面
            Transform healthPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.healthPanel);
            healthPanel.gameObject.AddComponent<HealthPanel>();
            // 加载摇杆界面
            Transform joystickPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.joystickPanel);
            joystickPanel.gameObject.AddComponent<JoystickPanel>();
            // 加载技能界面
            Transform skillPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.skillPanel);
            skillPanel.gameObject.AddComponent<SkillPanel>();
            // 加载暂停界面并设置隐藏
            Transform pauseMenuPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.PauseMenuPanel);
            pauseMenuPanel.gameObject.AddComponent<PauseMenuPanel>();
            PlayerManager.Instance.Holy.Data.IsGamePause = false;
            PlayerManager.Instance.Holy.Data.NotifyUI();
            // 加载小地图
            Transform litmapPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.litmapPanel);
            litmapPanel.gameObject.AddComponent<LitmapPanel>();
            // 销毁自己
            Destroy(gameObject);
        });
        // 绑定Quit按钮事件
        AddEventToWidget(quitButton, EventTriggerType.PointerClick, baseEventData =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
