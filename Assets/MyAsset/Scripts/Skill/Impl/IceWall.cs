using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IceWall : BaseSkill
{
    public IceWall()
    {
        index = 4;
        value = 7;
        name = "寒冰之墙";
        description = "科瑞克斯的杀戮之墙";
        cooldownTime = 25f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Ice_Wall_icon");
    }

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
    }
}
