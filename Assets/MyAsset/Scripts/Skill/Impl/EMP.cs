using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EMP : BaseSkill
{
    public EMP()
    {
        index = 3;
        value = 6;
        name = "电磁脉冲";
        description = "恩多利昂的恶之混动";
        cooldownTime = 30f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/EMP_icon");
    }

    private ParticleSystem particleSystem;

    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
        // 粒子特效
        if (particleSystem == null)
        {
            GameObject obj = ResourceManager.Instance.FindResource("Invoker/ParticleSystem/EMP");
            particleSystem = Object.Instantiate(obj).GetComponent<ParticleSystem>();
            //particleSystem.transform.SetParent(attacker.transform, false);
        }
        particleSystem.transform.position = attacker.transform.position;
        particleSystem.Play();
        // 生效逻辑
        Main.Instance.StartCoroutine(EffectCoro(attacker));
    }

    private Collider[] colliderArr;
    private NPCBaseCharacter character;
    private readonly float delayTime = 3.2f;
    private readonly float aoeRadius = 5f;
    private readonly int damage = 5;
    private IEnumerator EffectCoro(Transform attacker)
    {
        yield return new WaitForSeconds(delayTime);
        colliderArr = Physics.OverlapSphere(particleSystem.transform.position, aoeRadius, StringLayerName.LayerMask_Enemy);
        foreach (Collider collider in colliderArr)
        {
            character = collider.GetComponent<NPCBaseCharacter>();
            if (character == null)
            {
                Debug.Log($"{collider.name}没有NPCBaseCharacter组件");
            }
            else
            {
                character.GetHit(attacker, damage);
            }
        }
    }
}

