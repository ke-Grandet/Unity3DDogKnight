using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SkillPanel : UIBasePanel
{
    private SkillPanelBind skillPanelBind;

    public readonly Image[] slotImageArr = new Image[3];
    public readonly Image[] ultiImageArr = new Image[15];
    public Image quasImage;
    public Image wexImage;
    public Image exortImage;
    public Image invokeImage;
    public Image invokeCooldownImage;
    public Image ulti1Image;
    public Image ulti1CooldownImage;
    public Image ulti2Image;
    public Image ulti2CooldownImage;
    public Image runOnImage;
    public Image runOffImage;
    public Image attackImage;

    protected override void Awake()
    {
        base.Awake();
        skillPanelBind = new(this);
        InitWidget();
        BindListener();
        BindData();
    }

    private void InitWidget()
    {
        slotImageArr[0] = GetWidget<Image>("Slot1_N");
        slotImageArr[1] = GetWidget<Image>("Slot2_N");
        slotImageArr[2] = GetWidget<Image>("Slot3_N");
        quasImage = GetWidget<Image>("Quas_N");
        wexImage = GetWidget<Image>("Wex_N");
        exortImage = GetWidget<Image>("Exort_N");
        invokeImage = GetWidget<Image>("Invoke_N");
        invokeCooldownImage = GetWidget<Image>("Invoke_Cooldown_N");
        ulti1Image = GetWidget<Image>("Ulti1_N");
        ulti1CooldownImage = GetWidget<Image>("Ulti1_Cooldown_N");
        ulti2Image = GetWidget<Image>("Ulti2_N");
        ulti2CooldownImage = GetWidget<Image>("Ulti2_Cooldown_N");
        ulti1CooldownImage.fillAmount = 0;
        ulti2CooldownImage.fillAmount = 0;
        runOnImage = GetWidget<Image>("Run_On_N");
        runOffImage = GetWidget<Image>("Run_Off_N");
        runOnImage.gameObject.SetActive(false);
        runOffImage.gameObject.SetActive(true);
        attackImage = GetWidget<Image>("Attack_N");
    }

    // 添加事件
    private void BindListener()
    {
        AddEventToWidget(quasImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.SummonListener(ElementEnum.QUAS));
        AddEventToWidget(wexImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.SummonListener(ElementEnum.WEX));
        AddEventToWidget(exortImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.SummonListener(ElementEnum.EXORT));
        AddEventToWidget(invokeImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.InvokeListener());
        AddEventToWidget(ulti1Image, EventTriggerType.PointerClick, baseEventData => skillPanelBind.UltiListener(true));
        AddEventToWidget(ulti2Image, EventTriggerType.PointerClick, baseEventData => skillPanelBind.UltiListener(false));
        AddEventToWidget(runOnImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.RunListener(false));
        AddEventToWidget(runOffImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.RunListener(true));
        AddEventToWidget(attackImage, EventTriggerType.PointerClick, baseEventData => skillPanelBind.AttackListener());
    }

    // 注册委托
    private void BindData()
    {
        PlayerManager.Instance.PlayerCharacter.Data.AddEvent(data => skillPanelBind.UpdateRunImage(data));
        PlayerManager.Instance.PlayerCharacter.SkillData.AddEvent(data => skillPanelBind.UpdateElem(data));
        PlayerManager.Instance.PlayerCharacter.SkillData.AddEvent(data => skillPanelBind.UpdateInvoke(data));
        PlayerManager.Instance.PlayerCharacter.SkillData.AddEvent(data => skillPanelBind.UpdateUlti(data));
    }

    private void Update()
    {

    }
}
