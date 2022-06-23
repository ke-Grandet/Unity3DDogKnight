using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthPanel : UIBasePanel
{
    private Image portraitImage;
    private Image portraitGetHitImage;
    private Image healthBarMask;
    private TextMeshProUGUI waveText;

    private float originWidth;

    protected override void Awake()
    {
        base.Awake();
        InitWidget();
        BindEvent();
    }

    private void InitWidget()
    {
        portraitImage = GetWidget<Image>("PortraitImage_N");
        portraitGetHitImage = GetWidget<Image>("PortraitImage_GetHit_N");
        healthBarMask = GetWidget<Image>("HealthBarMask_N");
        waveText = GetWidget<TextMeshProUGUI>("Wave_N");
        originWidth = healthBarMask.rectTransform.rect.width;
        portraitGetHitImage.gameObject.SetActive(false);
    }

    private void BindEvent()
    {
        // 注册生命值变化的委托
        PlayerManager.Instance.PlayerCharacter.Data.AddEvent(data => 
        {
            healthBarMask.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, 
                originWidth * data.Health / data.MaxHealth);
        });
        // 注册无敌时间的委托
        PlayerManager.Instance.PlayerCharacter.Data.AddEvent(data =>
        {
            if (data.TimerInvincible > 0f || data.Health <= 0)
            {
                portraitImage.gameObject.SetActive(false);
                portraitGetHitImage.gameObject.SetActive(true);
            }
            else
            {
                portraitImage.gameObject.SetActive(true);
                portraitGetHitImage.gameObject.SetActive(false);
            }
        });
        // 注册波数的委托
        PlayerManager.Instance.Holy.Data.AddEvent(data =>
        {
            waveText.text = $"<color=red>Wave: <b>{data.DefendWave + 1} / {data.EnemyWaveCount}</b></color>";
            if (!data.IsWaveStart)
            {
                waveText.text += $"\nWave <b><color=yellow>{data.DefendWave + 1}</color></b> Coming: <color=red>{(int)data.TimerNextWave}</color> s";
            }
        });
    }
}