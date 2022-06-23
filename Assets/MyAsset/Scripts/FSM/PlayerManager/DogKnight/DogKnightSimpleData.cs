using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DogKnightSimpleData : BaseData<DogKnightSimpleData>
{
    // 移动相关
    public float Horizontal = 0f;
    public float Vertical = 0f;
    public int WalkSpeed = 2;
    public int RunSpeed = 4;
    public int SpeedLimit = 2;
    // 生命相关
    public int MaxHealth = 100;
    public int Health = 100;
    public int TakeDamage = 0;
    public float InvincibleTime = 0.5f;
    public float TimerInvincible = 0f;
    // 攻击相关
    public int AttackCombo = 0;  // 当前累计的连续攻击数，例如值为1时表示已进行过一次攻击
    public int AttackDamage = 10;
    public float AttackDistance = 2f;

    #region  访问器
    public bool IsMove { get { return !Mathf.Approximately(Horizontal, 0f) || !Mathf.Approximately(Vertical, 0f); } }
    public bool IsInvincible { get { return TimerInvincible > 0f; } }
    #endregion

    public string AsString()
    {
        return $"\nwalkspeed: {WalkSpeed}, \nrunSpeed: {RunSpeed}, \nSpeedlimt: {SpeedLimit}";
    }

    public void OnUpdate()
    {
        if (TimerInvincible > 0f)
        {
            TimerInvincible = Mathf.Max(0, TimerInvincible - Time.deltaTime);
            if (TimerInvincible == 0f)
            {
                NotifyUI();
            }
        }
    }
}
