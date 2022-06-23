using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class FailMenuPanel : UIBasePanel
{
    private TextMeshProUGUI scoreText;
    private Image restartButton;
    private Image quitButton;

    protected override void Awake()
    {
        base.Awake();
        InitWidget();
        BindListener();
    }

    public void InitWidget()
    {
        scoreText = GetWidget<TextMeshProUGUI>("Score_N");
        restartButton = GetWidget<Image>("Restart_N");
        quitButton = GetWidget<Image>("Quit_N");
        int wave = PlayerManager.Instance.Holy.Data.DefendWave;
        int count = PlayerManager.Instance.Holy.Data.DefeatEnemyCount;
        //string text = $"Waves: <color=yellow>{wave}</color>\nDefeat Enemies: <color=yellow>{count}</color>";
        string text2 = $"Waves: <color=yellow>{wave}</color>";
        scoreText.text = text2;
    }

    public void BindListener()
    {
        // 添加重新开始按钮的事件
        AddEventToWidget(restartButton, EventTriggerType.PointerClick, baseEventData =>
        {
            // 清除怪物
            NPCManager.Instance.ReturnAllNPC();
            // 恢复基地状态
            PlayerManager.Instance.Holy.Initial();
            // 复位玩家
            PlayerManager.Instance.Initial();
            // 开启进攻
            PlayerManager.Instance.Holy.StartDefense();
            // 销毁自己
            Destroy(gameObject);
        });
        // 添加结束按钮的事件
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
