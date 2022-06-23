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
        // ��Start��ť�¼�
        AddEventToWidget(startButton, EventTriggerType.PointerClick, baseEventData =>
        {
            // ��������
            //PlayerManager.Instance.Holy.StartDefense();
            // ��������ֵ����
            Transform healthPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.healthPanel);
            healthPanel.gameObject.AddComponent<HealthPanel>();
            // ����ҡ�˽���
            Transform joystickPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.joystickPanel);
            joystickPanel.gameObject.AddComponent<JoystickPanel>();
            // ���ؼ��ܽ���
            Transform skillPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.skillPanel);
            skillPanel.gameObject.AddComponent<SkillPanel>();
            // ������ͣ���沢��������
            Transform pauseMenuPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.PauseMenuPanel);
            pauseMenuPanel.gameObject.AddComponent<PauseMenuPanel>();
            PlayerManager.Instance.Holy.Data.IsGamePause = false;
            PlayerManager.Instance.Holy.Data.NotifyUI();
            // ����С��ͼ
            Transform litmapPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.litmapPanel);
            litmapPanel.gameObject.AddComponent<LitmapPanel>();
            // �����Լ�
            Destroy(gameObject);
        });
        // ��Quit��ť�¼�
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
