using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementEnum
{
    QUAS = 1, WEX = 2, EXORT = 5
}


[System.Serializable]
public class SkillData : BaseData<SkillData>
{
    public int[] ElemArr = { 0, 0, 0 };  // ������Ԫ��
    public int FirstIndex = 0;  // ���һ������Ԫ�ص�����
    public BaseSkill[] UltiArr = { null, null };  // ������ħ��
    public BaseSkill[] SkillArr = new BaseSkill[10];  // ������ħ��
    private int cooldownCount = 0;  // ��ȴ�еļ��ܵ���Ŀ
    private readonly float invokeCooldownTime = 2f;
    private float timerInvokeCooldown = 0f;

    public int CooldownCount { get { return cooldownCount; }  set { cooldownCount = value; } }
    public float InvokeCooldownTime { get { return invokeCooldownTime; } }
    public float TimerInvokeCooldown { get { return timerInvokeCooldown; } set { timerInvokeCooldown = value; } }
    public bool IsCooldown { get { return timerInvokeCooldown > 0; } }
    public int GetElemSum { get { return ElemArr[0] + ElemArr[1] + ElemArr[2]; } }
    public bool IsInvokeReady
    {
        get
        {
            foreach (int i in ElemArr)
            {
                if (i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }


    public SkillData()
    {
        SkillArr[0] = new ColdSnap();
        SkillArr[1] = new GhostWalk();
        SkillArr[2] = new Tornado();
        SkillArr[3] = new EMP();
        SkillArr[4] = new IceWall();
        SkillArr[5] = new DeafeningBlast();
        SkillArr[6] = new Alacrity();
        SkillArr[7] = new ForgeSpirit();
        SkillArr[8] = new ChaosMeteor();
        SkillArr[9] = new SunStrike();
    }

    public string AsString()
    {
        return $"�������λ�ã�{FirstIndex}; ����Ԫ�أ�[{ElemArr[0]}, {ElemArr[1]}, {ElemArr[2]}]";
    }

    public void OnUpdate()
    {
        // ��ħ������ȴ����ʱ
        if (timerInvokeCooldown > 0f)
        {
            timerInvokeCooldown = Mathf.Max(0f, timerInvokeCooldown - Time.deltaTime);
            NotifyUI();
        }
        // �����ļ��ܵ���ȴ����ʱ
        if (cooldownCount > 0)
        {
            foreach(BaseSkill skill in SkillArr)
            {
                if (skill.TimerCooldown > 0f)
                {
                    skill.TimerCooldown = Mathf.Max(0f, skill.TimerCooldown - Time.deltaTime);
                    if (skill.TimerCooldown == 0f)
                    {
                        cooldownCount--;
                    }
                }
                if (UltiArr[0] != null && skill.Value == UltiArr[0].Value 
                    || UltiArr[1] != null && skill.Value == UltiArr[1].Value)
                {
                    NotifyUI();
                }
            }
        }
    }
}
