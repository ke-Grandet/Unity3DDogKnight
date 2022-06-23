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
    /// ��Ԫ��
    /// </summary>
    /// <param name="elem"></param>
    public void Summon(ElementEnum elem)
    {
        character.SkillData.FirstIndex++;
        character.SkillData.FirstIndex %= character.SkillData.ElemArr.Length;
        character.SkillData.ElemArr[character.SkillData.FirstIndex] = (int)elem;
    }

    /// <summary>
    /// ��ħ�����м��ܣ�
    /// </summary>
    public void Invoke()
    {
        if (!character.SkillData.IsInvokeReady)
        {
            Debug.Log("Ҫ������Ԫ�ز�����ħ��");
            return;
        }
        if (character.SkillData.IsCooldown)
        {
            Debug.Log($"Ԫ����������ȴ������{character.SkillData.TimerInvokeCooldown:0.00}��");
            return;
        }
        if (character.SkillData.GetElemSum == character.SkillData.UltiArr[0]?.Value)
        {
            Debug.Log("�����ظ���ħ����" + character.SkillData.UltiArr[0].Name);
            return;
        }
        if (MapValueToIndex(character.SkillData.GetElemSum, out int index))
        {
            character.SkillData.UltiArr[1] = character.SkillData.UltiArr[0];
            character.SkillData.UltiArr[0] = character.SkillData.SkillArr[index];
            character.SkillData.TimerInvokeCooldown = character.SkillData.InvokeCooldownTime;
            Debug.Log($"�����˼��� {character.SkillData.UltiArr[0].Name} �� {character.SkillData.UltiArr[1]?.Name}");
        }
        else
        {
            Debug.Log("�����ڵļ��ܣ�" + character.SkillData.GetElemSum);
        }
    }

    /// <summary>
    /// �ͷ�ħ������һ����λ��
    /// </summary>
    public void ReleaseFirstSkill()
    {
        if (character.SkillData.UltiArr[0] == null)
        {
            Debug.Log("��Ҫ����һ��ħ��");
            return;
        }
        if (character.SkillData.UltiArr[0].IsCooldown)
        {
            Debug.Log($"{character.SkillData.UltiArr[0].Name}������ȴ������{character.SkillData.UltiArr[0].TimerCooldown:0.00}��");
            return;
        }
        character.SkillData.UltiArr[0].Effect(character.transform, character.transform);
        character.SkillData.CooldownCount++;
    }

    /// <summary>
    /// �ͷ�ħ�����ڶ�����λ��
    /// </summary>
    public void ReleaseSecondSkill()
    {
        if (character.SkillData.UltiArr[1] == null)
        {
            Debug.Log("��Ҫ��������ħ��");
            return;
        }
        if (character.SkillData.UltiArr[1].IsCooldown)
        {
            Debug.Log($"{character.SkillData.UltiArr[1].Name}������ȴ������{character.SkillData.UltiArr[1].TimerCooldown:0.00}��");
            return;
        }
        character.SkillData.UltiArr[1].Effect(character.transform, character.transform);
        character.SkillData.CooldownCount++;
    }

    /// <summary>
    /// �����ܵ�ֵת��Ϊ��Ӧ����������
    /// </summary>
    /// <param name="spell">���ܵ�ֵ</param>
    /// <param name="index">out���ⲿ����Ӧ����������</param>
    /// <returns>spell����Чֵʱ����false</returns>
    private bool MapValueToIndex(int spell, out int index)
    {
        switch (spell)
        {
            // ������ȴ Y
            case 3:
                //Debug.Log("��׿�����޾�ս����");
                index = 0;
                break;
            // �������� V
            case 4:
                //Debug.Log("������֮�谭��");
                index = 1;
                break;
            // ǿϮ쫷� X
            case 5:
                //Debug.Log("����³˹֮צ��");
                index = 2;
                break;
            // ������� C
            case 6:
                //Debug.Log("���������Ķ�֮�춯��");
                index = 3;
                break;
            // ����֮ǽ G
            case 7:
                //Debug.Log("�����˹��ɱ¾֮ǽ��");
                index = 4;
                break;
            // �������� B
            case 8:
                //Debug.Log("��³����֮����������");
                index = 5;
                break;
            // �鶯Ѹ�� Z
            case 9:
                //Debug.Log("��˹�ص���ս���");
                index = 6;
                break;
            // ��¯���� F
            case 11:
                //Debug.Log("����ά�����а���");
                index = 7;
                break;
            // ������ʯ D
            case 12:
                //Debug.Log("�����˵���׹֮��");
                index = 8;
                break;
            // ���׳�� T
            case 15:
                //Debug.Log("���׿�֮����ħ�䣡");
                index = 9;
                break;
            default:
                Debug.Log("��������������");
                index = -1;
                return false;
        }
        return true;
    }
}
