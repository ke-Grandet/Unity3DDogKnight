using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelBind
{
    private readonly SkillPanel skillPanel;

    public SkillPanelBind(SkillPanel skillPanel)
    {
        this.skillPanel = skillPanel;
    }

    /// <summary>
    /// ����¼�������
    /// </summary>
    public void AttackListener()
    {
        PlayerManager.Instance.PlayerCharacter.CommandState(StateEnum.ATTACK);
    }

    /// <summary>
    /// ����¼��������ƶ�
    /// </summary>
    /// <param name="isRunOn"></param>
    public void RunListener(bool isRunOn)
    {
        if (PlayerManager.Instance.PlayerCharacter.Data.IsMove)
        {
            PlayerManager.Instance.PlayerCharacter.CommandState(isRunOn ? StateEnum.RUN : StateEnum.WALK);
        }
    }

    /// <summary>
    /// ����¼�����Ԫ��
    /// </summary>
    /// <param name="elem">Ԫ������</param>
    public void SummonListener(ElementEnum elem)
    {
        PlayerManager.Instance.PlayerCharacter.SkillLogic.Summon(elem);
        UpdateElem(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// ����¼�����ħ�����м��ܣ�
    /// </summary>
    public void InvokeListener()
    {
        Debug.Log("������Ԫ����");
        PlayerManager.Instance.PlayerCharacter.SkillLogic.Invoke();
        UpdateInvoke(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// ����¼����ͷ�ħ�����ż��ܣ�
    /// </summary>
    /// <param name="index"></param>
    public void UltiListener(bool isFirstSkill)
    {
        if (isFirstSkill)
        {
            Debug.Log("�����˼���1");
            PlayerManager.Instance.PlayerCharacter.SkillLogic.ReleaseFirstSkill();
        }
        else
        {
            Debug.Log("�����˼���2");
            PlayerManager.Instance.PlayerCharacter.SkillLogic.ReleaseSecondSkill();
        }
        UpdateUlti(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// ί�з��������¼����ƶ�״̬ͼ��
    /// </summary>
    /// <param name="data"></param>
    public void UpdateRunImage(DogKnightSimpleData data)
    {
        //Debug.Log("�����ƶ���");
        if (data.SpeedLimit <= data.WalkSpeed)
        {
            skillPanel.runOnImage.gameObject.SetActive(false);
            skillPanel.runOffImage.gameObject.SetActive(true);
        }
        else if (data.SpeedLimit > data.WalkSpeed)
        {
            skillPanel.runOnImage.gameObject.SetActive(true);
            skillPanel.runOffImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ί�з���������Ԫ�ز�
    /// </summary>
    /// <param name="data"></param>
    public void UpdateElem(SkillData data)
    {
        int index;
        for (int i = 0; i < data.ElemArr.Length; i++)
        {
            index = (data.FirstIndex - i + data.ElemArr.Length) % data.ElemArr.Length;
            switch (data.ElemArr[index])
            {
                case (int)ElementEnum.QUAS:
                    skillPanel.slotImageArr[i].sprite = skillPanel.quasImage.sprite;
                    break;
                case (int)ElementEnum.WEX:
                    skillPanel.slotImageArr[i].sprite = skillPanel.wexImage.sprite;
                    break;
                case (int)ElementEnum.EXORT:
                    skillPanel.slotImageArr[i].sprite = skillPanel.exortImage.sprite;
                    break;
            }
        }
    }

    /// <summary>
    /// ί�з�����������ħ����CD��ʾ
    /// </summary>
    /// <param name="data"></param>
    public void UpdateInvoke(SkillData data)
    {
        if (data.IsCooldown)
        {
            //skillPanel.invokeImage.raycastTarget = false;
            skillPanel.invokeCooldownImage.gameObject.SetActive(true);
            skillPanel.invokeCooldownImage.fillAmount = data.TimerInvokeCooldown / data.InvokeCooldownTime;
        }
        else
        {
            //skillPanel.invokeImage.raycastTarget = true;
            skillPanel.invokeCooldownImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ί�з��������¼��ܲ۵ĵ�ǰ���ܺ�CD��ʾ
    /// </summary>
    /// <param name="data"></param>
    public void UpdateUlti(SkillData data)
    {
        // ����1
        if (data.UltiArr[0] != null)
        {
            // ����ͼ��
            if (skillPanel.ulti1Image.sprite != data.UltiArr[0].SkillImage)
            {
                skillPanel.ulti1Image.sprite = data.UltiArr[0].SkillImage;
                skillPanel.ulti1CooldownImage.sprite = data.UltiArr[0].SkillImage;
            }
            // ������ȴ״̬
            if (data.UltiArr[0].IsCooldown)
            {
                //skillPanel.ulti1Image.raycastTarget = false;
                skillPanel.ulti1CooldownImage.gameObject.SetActive(true);
                skillPanel.ulti1CooldownImage.fillAmount = data.UltiArr[0].TimerCooldown / data.UltiArr[0].CooldownTime;
            }
            else
            {
                //skillPanel.ulti1Image.raycastTarget = true;
                skillPanel.ulti1CooldownImage.gameObject.SetActive(false);
            }
        }
        // ����2
        if (data.UltiArr[1] != null)
        {
            // ����ͼ��
            if (skillPanel.ulti2Image.sprite != data.UltiArr[1].SkillImage)
            {
                skillPanel.ulti2Image.sprite = data.UltiArr[1].SkillImage;
                skillPanel.ulti2CooldownImage.sprite = data.UltiArr[1].SkillImage;
            }
            // ������ȴ״̬
            if (data.UltiArr[1].IsCooldown)
            {
                //skillPanel.ulti2Image.raycastTarget = false;
                skillPanel.ulti2CooldownImage.gameObject.SetActive(true);
                skillPanel.ulti2CooldownImage.fillAmount = data.UltiArr[1].TimerCooldown / data.UltiArr[1].CooldownTime;
            }
            else
            {
                //skillPanel.ulti2Image.raycastTarget = true;
                skillPanel.ulti2CooldownImage.gameObject.SetActive(false);
            }
        }
    }

}
