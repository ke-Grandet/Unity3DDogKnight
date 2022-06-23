using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChaosMeteor : BaseSkill
{
    public ChaosMeteor()
    {
        index = 8;
        value = 12;
        name = "混沌陨石";
        description = "塔拉克的天坠之火";
        cooldownTime = 55f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Chaos_Meteor_icon");
    }

    private ParticleSystem particleSystem;
    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}释放了{name}!");
        // 粒子特效
        if (particleSystem == null)
        {
            GameObject obj = ResourceManager.Instance.FindResource("Invoker/ParticleSystem/ChaosMeteor");
            particleSystem = Object.Instantiate(obj).GetComponent<ParticleSystem>();
        }
        particleSystem.transform.position = attacker.transform.position + Vector3.up * 3;
        particleSystem.Play();
        // 生效逻辑
        Main.Instance.StartCoroutine(EffectCoro(attacker, attacker.forward));
    }

    private readonly float effectTime = 5f;  // 作用时间
    private readonly int damage = 10;  // 伤害值
    private readonly float damageTime = 1f;  // 伤害间隔
    private readonly float moveSpeed = 1f;  // 移动速度
    private readonly float effectRadius = 2f;  // 伤害范围
    private Collider[] colliderArr;
    private NPCBaseCharacter character;
    private float timerEffect = 0f;
    private float timerDamage = 0f;
    private Vector3 deltaPosition = Vector3.zero;
    private IEnumerator EffectCoro(Transform attacker, Vector3 direction)
    {
        timerEffect = effectTime;
        timerDamage = 0f;
        while (timerEffect > 0f)
        {
            timerEffect -= Time.deltaTime;
            // 持续移动
            deltaPosition = new(moveSpeed * Time.deltaTime * direction.x,
                particleSystem.transform.position.y <= 1f ? 0f : - moveSpeed * 2 * Time.deltaTime,
                moveSpeed * Time.deltaTime * direction.z);
            particleSystem.transform.position += deltaPosition;
            //Debug.DrawRay(particleSystem.transform.position, direction * effectRadius, Color.green);
            //Debug.DrawRay(particleSystem.transform.position, -direction * effectRadius, Color.green);
            // 每秒一次伤害检测
            timerDamage = Mathf.Max(0f, timerDamage - Time.deltaTime);
            if (timerDamage <= 0f)
            {
                colliderArr = Physics.OverlapSphere(particleSystem.transform.position, effectRadius, StringLayerName.LayerMask_Enemy);
                //Debug.Log("击中敌人数：" + colliderArr.Length);
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
                timerDamage = damageTime;
            }
            yield return new WaitForEndOfFrame();
        }
        particleSystem.Stop();
    }
}

