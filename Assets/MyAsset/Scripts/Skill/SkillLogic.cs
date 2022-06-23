using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillLogic
{
    private readonly DogKnightCharacter character;

    public SkillLogic(DogKnightCharacter character)
    {
        this.character = character;
    }

    /// <summary>
    /// 祈唤元素
    /// </summary>
    /// <param name="elem"></param>
    public void Summon(ElementEnum elem)
    {
        character.SkillData.FirstIndex++;
        character.SkillData.FirstIndex %= character.SkillData.ElemArr.Length;
        character.SkillData.ElemArr[character.SkillData.FirstIndex] = (int)elem;
    }

    /// <summary>
    /// 祈唤魔法（切技能）
    /// </summary>
    public void Invoke()
    {
        if (!character.SkillData.IsInvokeReady)
        {
            Debug.Log("要有三个元素才能祈唤魔法");
            return;
        }
        if (character.SkillData.IsCooldown)
        {
            Debug.Log($"元素祈唤正在冷却，还有{character.SkillData.TimerInvokeCooldown:0.00}秒");
            return;
        }
        if (character.SkillData.GetElemSum == character.SkillData.UltiArr[0]?.Value)
        {
            Debug.Log("祈唤了重复的魔法：" + character.SkillData.UltiArr[0].Name);
            return;
        }
        if (MapValueToIndex(character.SkillData.GetElemSum, out int index))
        {
            character.SkillData.UltiArr[1] = character.SkillData.UltiArr[0];
            character.SkillData.UltiArr[0] = character.SkillData.SkillArr[index];
            character.SkillData.TimerInvokeCooldown = character.SkillData.InvokeCooldownTime;
            Debug.Log($"我切了技能 {character.SkillData.UltiArr[0].Name} 和 {character.SkillData.UltiArr[1]?.Name}");
        }
        else
        {
            Debug.Log("不存在的技能：" + character.SkillData.GetElemSum);
        }
    }

    /// <summary>
    /// 释放魔法（第一个槽位）
    /// </summary>
    public void ReleaseFirstSkill()
    {
        if (character.SkillData.UltiArr[0] == null)
        {
            Debug.Log("需要先祈唤一个魔法");
            return;
        }
        if (character.SkillData.UltiArr[0].IsCooldown)
        {
            Debug.Log($"{character.SkillData.UltiArr[0].Name}正在冷却，还有{character.SkillData.UltiArr[0].TimerCooldown:0.00}秒");
            return;
        }
        character.SkillData.UltiArr[0].Effect(character.transform, character.transform);
        character.SkillData.CooldownCount++;
    }

    /// <summary>
    /// 释放魔法（第二个槽位）
    /// </summary>
    public void ReleaseSecondSkill()
    {
        if (character.SkillData.UltiArr[1] == null)
        {
            Debug.Log("需要先祈唤两个魔法");
            return;
        }
        if (character.SkillData.UltiArr[1].IsCooldown)
        {
            Debug.Log($"{character.SkillData.UltiArr[1].Name}正在冷却，还有{character.SkillData.UltiArr[1].TimerCooldown:0.00}秒");
            return;
        }
        character.SkillData.UltiArr[1].Effect(character.transform, character.transform);
        character.SkillData.CooldownCount++;
    }

    /// <summary>
    /// 将技能的值转换为对应的数组索引
    /// </summary>
    /// <param name="spell">技能的值</param>
    /// <param name="index">out给外部：对应的数组索引</param>
    /// <returns>spell是无效值时返回false</returns>
    private bool MapValueToIndex(int spell, out int index)
    {
        switch (spell)
        {
            // 急速冷却 Y
            case 3:
                //Debug.Log("塞卓昂的无尽战栗！");
                index = 0;
                break;
            // 幽灵漫步 V
            case 4:
                //Debug.Log("米瑞特之阻碍！");
                index = 1;
                break;
            // 强袭飓风 X
            case 5:
                //Debug.Log("托纳鲁斯之爪！");
                index = 2;
                break;
            // 电磁脉冲 C
            case 6:
                //Debug.Log("恩多利昂的恶之混动！");
                index = 3;
                break;
            // 寒冰之墙 G
            case 7:
                //Debug.Log("科瑞克斯的杀戮之墙！");
                index = 4;
                break;
            // 超震声波 B
            case 8:
                //Debug.Log("布鲁冯特之无力声波！");
                index = 5;
                break;
            // 灵动迅捷 Z
            case 9:
                //Debug.Log("盖斯特的猛战号令！");
                index = 6;
                break;
            // 熔炉精灵 F
            case 11:
                //Debug.Log("卡尔维因的至邪造物！");
                index = 7;
                break;
            // 混沌陨石 D
            case 12:
                //Debug.Log("塔拉克的天坠之火！");
                index = 8;
                break;
            // 阳炎冲击 T
            case 15:
                //Debug.Log("哈雷克之火葬魔咒！");
                index = 9;
                break;
            default:
                Debug.Log("倒背如流的咒语");
                index = -1;
                return false;
        }
        return true;
    }
}
