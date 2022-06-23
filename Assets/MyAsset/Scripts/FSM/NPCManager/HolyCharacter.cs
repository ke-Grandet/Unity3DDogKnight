using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class HolySimpleData : BaseData<HolySimpleData>
{
    public int MaxHealth = 50;
    public int Health = 50;

    public int EnemyWaveCount = 10;  // �ܼ�10������
    public int EnemyComeCount = 8;  // ÿ��8�ν���
    public float EnemyWaveInterval = 60f;  // ÿ���������60��
    public float EnemyComeInterval = 5f;  // ÿ�ν������5��

    public float TimerNextWave = 0f;  // ����һ������֮ǰ��ʣ��ʱ��
    public int DefendWave = 0;  // �ѷ��صĲ���
    public int DefeatEnemyCount = 0;  // ���ܵ�����

    public bool IsGameOver = false;
    public bool IsGamePause = false;

    public bool IsWaveStart { get { return TimerNextWave <= 0; } }
}


public class HolyCharacter : NPCBaseCharacter
{
    private HolySimpleData data;
    private NPCTrigger npcTrigger;
    private Coroutine defenseCoro;

    public HolySimpleData Data { get { return data; } }

    protected override void Awake()
    {
        base.Awake();
        NPCManager.Instance.UnRegistNPC(this);
        npcTrigger = new();
        data = ResourceManager.Instance.FindResourceFromJson<HolySimpleData>(StringDataPath.Holy_Simple_Data);
        Initial();
    }

    public override void Initial()
    {
        data.Health = data.MaxHealth;
        data.DefendWave = 0;
        data.DefeatEnemyCount = 0;
        data.IsGameOver = false;
        data.IsGamePause = false;
    }

    public override void GetHit(Transform attacker, int damage)
    {
        base.GetHit(attacker, damage);
        data.Health -= damage;
        if (data.Health <= 0)
        {
            GameOver();
        }
    }

    public void StartDefense()
    {
        defenseCoro = StartCoroutine(StartDefendCoro());
    }

    private IEnumerator StartDefendCoro()
    {
        for (data.DefendWave = 0; data.DefendWave < data.EnemyWaveCount; data.DefendWave++)
        {
            data.TimerNextWave = data.EnemyWaveInterval;
            data.NotifyUI();
            while (data.TimerNextWave > 0)
            {
                yield return new WaitForSeconds(1);
                data.TimerNextWave--;
                data.NotifyUI();
            }
            for (int i = 0; i < data.EnemyComeCount; i++)
            {
                npcTrigger.EnemyCome(transform);
                yield return new WaitForSeconds(data.EnemyComeInterval);
            }
        }
    }

    public void GameOver()
    {
        if (!data.IsGameOver)
        {
            data.IsGameOver = true;
            StopCoroutine(defenseCoro);
            Transform failMenuPanel = UIManager.Instance.LoadPanel(StringUIPanelPath.FailMenuPanel);
            failMenuPanel.gameObject.AddComponent<FailMenuPanel>();
        }
    }

    protected override void Update()
    {
        // ����Ѫ��
        UpdateHealthBar(data.Health, data.MaxHealth);
        // �����������
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    npcTrigger.EnemyCome(transform);
        //}
        // ������Ϸ��ͣ
        if (Input.GetKeyDown(KeyCode.Escape) && !data.IsGameOver)
        {
            data.IsGamePause = !data.IsGamePause;
            data.NotifyUI();
        }
    }
}
