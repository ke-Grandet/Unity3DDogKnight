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
        name = "������ʯ";
        description = "�����˵���׹֮��";
        cooldownTime = 55f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Chaos_Meteor_icon");
    }

    private ParticleSystem particleSystem;
    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}�ͷ���{name}!");
        // ������Ч
        if (particleSystem == null)
        {
            GameObject obj = ResourceManager.Instance.FindResource("Invoker/ParticleSystem/ChaosMeteor");
            particleSystem = Object.Instantiate(obj).GetComponent<ParticleSystem>();
        }
        particleSystem.transform.position = attacker.transform.position + Vector3.up * 3;
        particleSystem.Play();
        // ��Ч�߼�
        Main.Instance.StartCoroutine(EffectCoro(attacker, attacker.forward));
    }

    private readonly float effectTime = 5f;  // ����ʱ��
    private readonly int damage = 10;  // �˺�ֵ
    private readonly float damageTime = 1f;  // �˺����
    private readonly float moveSpeed = 1f;  // �ƶ��ٶ�
    private readonly float effectRadius = 2f;  // �˺���Χ
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
            // �����ƶ�
            deltaPosition = new(moveSpeed * Time.deltaTime * direction.x,
                particleSystem.transform.position.y <= 1f ? 0f : - moveSpeed * 2 * Time.deltaTime,
                moveSpeed * Time.deltaTime * direction.z);
            particleSystem.transform.position += deltaPosition;
            //Debug.DrawRay(particleSystem.transform.position, direction * effectRadius, Color.green);
            //Debug.DrawRay(particleSystem.transform.position, -direction * effectRadius, Color.green);
            // ÿ��һ���˺����
            timerDamage = Mathf.Max(0f, timerDamage - Time.deltaTime);
            if (timerDamage <= 0f)
            {
                colliderArr = Physics.OverlapSphere(particleSystem.transform.position, effectRadius, StringLayerName.LayerMask_Enemy);
                //Debug.Log("���е�������" + colliderArr.Length);
                foreach (Collider collider in colliderArr)
                {
                    character = collider.GetComponent<NPCBaseCharacter>();
                    if (character == null)
                    {
                        Debug.Log($"{collider.name}û��NPCBaseCharacter���");
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

