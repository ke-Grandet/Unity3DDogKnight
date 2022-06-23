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
    /// 点击事件：攻击
    /// </summary>
    public void AttackListener()
    {
        PlayerManager.Instance.PlayerCharacter.CommandState(StateEnum.ATTACK);
    }

    /// <summary>
    /// 点击事件：加速移动
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
    /// 点击事件：祈唤元素
    /// </summary>
    /// <param name="elem">元素类型</param>
    public void SummonListener(ElementEnum elem)
    {
        PlayerManager.Instance.PlayerCharacter.SkillLogic.Summon(elem);
        UpdateElem(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// 点击事件：祈唤魔法（切技能）
    /// </summary>
    public void InvokeListener()
    {
        Debug.Log("你点击了元素祈唤");
        PlayerManager.Instance.PlayerCharacter.SkillLogic.Invoke();
        UpdateInvoke(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// 点击事件：释放魔法（放技能）
    /// </summary>
    /// <param name="index"></param>
    public void UltiListener(bool isFirstSkill)
    {
        if (isFirstSkill)
        {
            Debug.Log("你点击了技能1");
            PlayerManager.Instance.PlayerCharacter.SkillLogic.ReleaseFirstSkill();
        }
        else
        {
            Debug.Log("你点击了技能2");
            PlayerManager.Instance.PlayerCharacter.SkillLogic.ReleaseSecondSkill();
        }
        UpdateUlti(PlayerManager.Instance.PlayerCharacter.SkillData);
    }

    /// <summary>
    /// 委托方法：更新加速移动状态图标
    /// </summary>
    /// <param name="data"></param>
    public void UpdateRunImage(DogKnightSimpleData data)
    {
        //Debug.Log("加速移动！");
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
    /// 委托方法：更新元素槽
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
    /// 委托方法：更新祈唤魔法的CD显示
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
    /// 委托方法：更新技能槽的当前技能和CD显示
    /// </summary>
    /// <param name="data"></param>
    public void UpdateUlti(SkillData data)
    {
        // 技能1
        if (data.UltiArr[0] != null)
        {
            // 更新图标
            if (skillPanel.ulti1Image.sprite != data.UltiArr[0].SkillImage)
            {
                skillPanel.ulti1Image.sprite = data.UltiArr[0].SkillImage;
                skillPanel.ulti1CooldownImage.sprite = data.UltiArr[0].SkillImage;
            }
            // 更新冷却状态
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
        // 技能2
        if (data.UltiArr[1] != null)
        {
            // 更新图标
            if (skillPanel.ulti2Image.sprite != data.UltiArr[1].SkillImage)
            {
                skillPanel.ulti2Image.sprite = data.UltiArr[1].SkillImage;
                skillPanel.ulti2CooldownImage.sprite = data.UltiArr[1].SkillImage;
            }
            // 更新冷却状态
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
