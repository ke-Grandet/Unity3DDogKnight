using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : SingletonMono<Main>
{
    public MonoBehaviour Mono { get { return this; } }
    protected override void Awake()
    {
        base.Awake();
        gameObject.AddComponent<UIManager>();
        // 取消敌人之间的碰撞
        Physics.IgnoreLayerCollision(StringLayerName.Layer_Enemy, StringLayerName.Layer_Enemy);
        Physics.IgnoreLayerCollision(StringLayerName.Layer_Enemy, StringLayerName.Layer_Holy);
        // 播放背景音乐
        AudioManager.Instance.PlayBackgroundMusic(StringAudioName.Normal_Music);
        // 在画布中初始化一个主界面
        Transform mainMenuPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.MainMenuPanel);
        mainMenuPanel.gameObject.AddComponent<MainMenuPanel>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerManager.Instance.Holy.StartDefense();
        }
    }

    private void LateUpdate()
    {
        CameraManager.Instance.OnLateUpdate();
    }
}
