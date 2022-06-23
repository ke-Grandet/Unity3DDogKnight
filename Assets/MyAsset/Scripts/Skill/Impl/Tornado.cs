using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tornado : BaseSkill
{
    public Tornado()
    {
        index = 2;
        value = 5;
        name = "强袭飓风";
        description = "托纳鲁斯之爪";
        cooldownTime = 30f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Tornado_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
    }
}

