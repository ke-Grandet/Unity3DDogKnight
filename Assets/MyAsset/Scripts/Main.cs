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
        // ȡ������֮�����ײ
        Physics.IgnoreLayerCollision(StringLayerName.Layer_Enemy, StringLayerName.Layer_Enemy);
        Physics.IgnoreLayerCollision(StringLayerName.Layer_Enemy, StringLayerName.Layer_Holy);
        // ���ű�������
        AudioManager.Instance.PlayBackgroundMusic(StringAudioName.Normal_Music);
        // �ڻ����г�ʼ��һ��������
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
