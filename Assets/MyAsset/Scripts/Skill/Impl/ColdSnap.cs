using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColdSnap : BaseSkill
{
    public ColdSnap()
    {
        index = 0;
        value = 3;
        name = "������ȴ";
        description = "��׿�����޾�ս��";
        cooldownTime = 20f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Cold_Snap_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}�ͷ���{name}!");
    }
}
