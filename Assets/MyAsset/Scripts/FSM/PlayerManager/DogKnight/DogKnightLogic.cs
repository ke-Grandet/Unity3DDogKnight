using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DogKnightLogic
{
    private readonly DogKnightCharacter character;
    public DogKnightLogic(DogKnightCharacter character)
    {
        this.character = character;
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    public void Move(float minSpeed = 0)
    {
        // �ı�λ��
        float speedPercent = Mathf.Sqrt(character.Data.Horizontal * character.Data.Horizontal + character.Data.Vertical * character.Data.Vertical);
        float speed = Mathf.Max(character.Data.SpeedLimit * speedPercent, minSpeed);
        character.CharacterController.SimpleMove(speed * character.transform.forward);
        // �ı䳯��
        float angle = Mathf.Atan2(character.Data.Vertical, character.Data.Horizontal) * Mathf.Rad2Deg;
        Rotate(angle);
    }

    /// <summary>
    /// ��ת
    /// </summary>
    /// <param name="angle">��ת�Ƕ�</param>
    public void Rotate(float angle)
    {
        Vector3 originAngle = character.transform.localEulerAngles;
        originAngle.y = -angle + 90;
        character.transform.localEulerAngles = originAngle;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Attack()
    {
        if (Physics.Raycast(
                new(character.transform.position + Vector3.up, character.transform.forward),
                out RaycastHit hit,
                character.Data.AttackDistance,
                StringLayerName.LayerMask_Enemy
            ))
        {
            //Debug.Log($"{character.name}����������Ŀ��{hit.transform.name}");
            NPCBaseCharacter enemy = hit.transform.GetComponent<NPCBaseCharacter>();
            enemy.GetHit(character.transform, character.Data.AttackDamage);
            character.Data.AttackCombo++;
            character.Data.AttackCombo %= 2;
        }
        else
        {
            //Debug.Log($"{character.name}������������������ˣ�");
            character.Data.AttackCombo = 0;
        }
    }

    public void AttackSecond()
    {
        Collider[] colliderArr = Physics.OverlapSphere(character.transform.position + Vector3.up, character.Data.AttackDistance, StringLayerName.LayerMask_Enemy);
        foreach (Collider collider in colliderArr)
        {
            if (AoeCheck.SectorAttack(character.transform, collider.transform, character.Data.AttackDistance, 120))
            {
                collider.GetComponent<BaseCharacter>().GetHit(character.transform, character.Data.AttackDamage);
            }
        }
        //Debug.Log($"{character.name}���ι���������{colliderArr.Length}��");
        character.Data.AttackCombo = 0;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void GetHit()
    {
        if (character.Data.IsInvincible)
        {
            return;
        }
        //Debug.Log($"{character.name}�ܵ�{character.Data.TakeDamage}�˺�");
        character.Data.TimerInvincible = character.Data.InvincibleTime;
        character.Data.Health = Mathf.Max(0, character.Data.Health - character.Data.TakeDamage);
        character.Data.TakeDamage = 0;
    }

}
