using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class HolySimpleData : BaseData<HolySimpleData>
{
    public int MaxHealth = 50;
    public int Health = 50;

    public int EnemyWaveCount = 10;  // 总计10波进攻
    public int EnemyComeCount = 8;  // 每波8次进攻
    public float EnemyWaveInterval = 60f;  // 每波进攻间隔60秒
    public float EnemyComeInterval = 5f;  // 每次进攻间隔5秒

    public float TimerNextWave = 0f;  // 离下一波进攻之前的剩余时间
    public int DefendWave = 0;  // 已防守的波数
    public int DefeatEnemyCount = 0;  // 击败敌人数

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
        // 更新血条
        UpdateHealthBar(data.Health, data.MaxHealth);
        // 触发怪物进攻
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    npcTrigger.EnemyCome(transform);
        //}
        // 按键游戏暂停
        if (Input.GetKeyDown(KeyCode.Escape) && !data.IsGameOver)
        {
            data.IsGamePause = !data.IsGamePause;
            data.NotifyUI();
        }
    }
}
