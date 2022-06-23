using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GhostWalk : BaseSkill
{
    public GhostWalk()
    {
        index = 1;
        value = 4;
        name = "��������";
        description = "������֮�谭";
        cooldownTime = 45f;
        skillImage = ResourceManager.Instance.FindSprite("Invoker/Ulti/Ghost_Walk_icon");
    }

    private ParticleSystem particleSystem;
    public override void Effect(Transform attacker, Transform target)
    {
        base.Effect(attacker, target);
        Debug.Log($"{attacker.name}�ͷ���{name}!");
        // ������Ч
        if (particleSystem == null)
        {
            GameObject obj = ResourceManager.Instance.FindResource("Invoker/ParticleSystem/GhostWalk");
            particleSystem = Object.Instantiate(obj).GetComponent<ParticleSystem>();
            particleSystem.transform.SetParent(attacker.transform, false);
            particleSystem.transform.localPosition += Vector3.up;
        }
        particleSystem.transform.position = attacker.transform.position + Vector3.up * 3;
        particleSystem.Play();
        // ��Ч�߼�
        Main.Instance.StartCoroutine(EffectCoro(attacker));
    }

    private readonly float invincibleTime = 5f;
    private DogKnightCharacter character;
    private IEnumerator EffectCoro(Transform attacker)
    {
        character = attacker.GetComponent<DogKnightCharacter>();
        if (character == null)
        {
            Debug.Log("û��character");
        }
        else
        {
            character.Data.TimerInvincible = invincibleTime;
            character.Data.NotifyUI();
        }
        yield return new WaitForSeconds(invincibleTime);
        particleSystem.Stop();
    }
}

