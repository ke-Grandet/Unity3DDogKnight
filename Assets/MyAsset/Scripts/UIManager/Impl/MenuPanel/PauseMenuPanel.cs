using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class PauseMenuPanel : UIBasePanel
{
    private Image resumeButton;
    private Image quitButton;

    protected override void Awake()
    {
        base.Awake();
        InitWidget();
        BindListener();

    }

    private void InitWidget()
    {
        resumeButton = GetWidget<Image>("Resume_N");
        quitButton = GetWidget<Image>("Quit_N");
    }

    private void BindListener()
    {
        // 添加事件
        AddEventToWidget(resumeButton, EventTriggerType.PointerClick, baseEventData =>
        {
            Time.timeScale = 1;
            PlayerManager.Instance.Holy.Data.IsGamePause = false;
            gameObject.SetActive(false);
        });
        AddEventToWidget(quitButton, EventTriggerType.PointerClick, baseEventData =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        // 注册委托
        PlayerManager.Instance.Holy.Data.AddEvent(data =>
        {
            Time.timeScale = data.IsGamePause ? 0 : 1;
            gameObject.SetActive(data.IsGamePause);
        });
    }
}
