using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SunStrike : BaseSkill
{
    public SunStrike()
    {
        index = 9;
        value = 15;
        name = "���׳��";
        description = "���׿�֮����ħ��";
        cooldownTime = 25f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Sun_Strike_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}�ͷ���{name}!");
    }
}

