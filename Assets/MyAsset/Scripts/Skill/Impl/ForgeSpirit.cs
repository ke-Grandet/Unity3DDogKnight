using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ForgeSpirit : BaseSkill
{
    public ForgeSpirit()
    {
        index = 7;
        value = 11;
        name = "熔炉精灵";
        description = "卡尔维因的至邪造物";
        cooldownTime = 30f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Forge_Spirit_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
    }
}

