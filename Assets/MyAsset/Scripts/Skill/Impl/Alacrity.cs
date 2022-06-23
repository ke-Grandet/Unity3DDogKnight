using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Alacrity : BaseSkill
{
    public Alacrity()
    {
        index = 6;
        value = 9;
        name = "�鶯Ѹ��";
        description = "��˹�ص���ս����";
        cooldownTime = 17f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Alacrity_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}�ͷ���{name}!");
    }
}

