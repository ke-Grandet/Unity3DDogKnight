using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeafeningBlast : BaseSkill
{
    public DeafeningBlast()
    {
        index = 5;
        value = 8;
        name = "超震声波";
        description = "布鲁冯特之无力声波";
        cooldownTime = 40f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Deafening_Blast_icon");
    }

    private Collider[] ColliderArr;
    private readonly float aoeRadius = 5f;
    private NPCBaseCharacter character;
    private ParticleSystem particleSystem;
    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
        ColliderArr = Physics.OverlapSphere(attacker.position, aoeRadius, StringLayerName.LayerMask_Enemy);
        // 粒子特效
        if (particleSystem == null)
        {
            GameObject obj = ResourceManager.Instance.FindResource("Invoker/ParticleSystem/DeafeningBlast");
            particleSystem = Object.Instantiate(obj).GetComponent<ParticleSystem>();
            particleSystem.transform.SetParent(attacker.transform, false);
        }
        particleSystem.Play();
        // 生效逻辑
        foreach (Collider collider in ColliderArr)
        {
            //Debug.Log("攻击到了" + collider.name);
            character = collider.GetComponent<NPCBaseCharacter>();
            if (character == null)
            {
                Debug.Log($"{collider.name}没有NPCBaseCharacter组件");
            }
            else
            {
                Main.Instance.StartCoroutine(EffectCoro(attacker, character));
            }
        }
    }

    private readonly float effectTime = 2f;  // 作用时间
    private readonly float moveSpeed = 1f;  // 击退速度
    private IEnumerator EffectCoro(Transform attacker, NPCBaseCharacter character)
    {
        character.IsControlled = true;
        character.FSM.ChangeState(StateEnum.IDLE);
        float timerEffect = effectTime;
        while (timerEffect > 0f)
        {
            timerEffect = Mathf.Max(0f, timerEffect - Time.deltaTime);
            character.CharacterController.SimpleMove((character.transform.position - attacker.position).normalized * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
        character.IsControlled = false;
    }
}
