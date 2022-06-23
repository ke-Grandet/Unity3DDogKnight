using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DogKnightSimpleData : BaseData<DogKnightSimpleData>
{
    // �ƶ����
    public float Horizontal = 0f;
    public float Vertical = 0f;
    public int WalkSpeed = 2;
    public int RunSpeed = 4;
    public int SpeedLimit = 2;
    // �������
    public int MaxHealth = 100;
    public int Health = 100;
    public int TakeDamage = 0;
    public float InvincibleTime = 0.5f;
    public float TimerInvincible = 0f;
    // �������
    public int AttackCombo = 0;  // ��ǰ�ۼƵ�����������������ֵΪ1ʱ��ʾ�ѽ��й�һ�ι���
    public int AttackDamage = 10;
    public float AttackDistance = 2f;

    #region  ������
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
