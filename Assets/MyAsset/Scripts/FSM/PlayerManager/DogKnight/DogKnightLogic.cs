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
    /// 移动
    /// </summary>
    public void Move(float minSpeed = 0)
    {
        // 改变位置
        float speedPercent = Mathf.Sqrt(character.Data.Horizontal * character.Data.Horizontal + character.Data.Vertical * character.Data.Vertical);
        float speed = Mathf.Max(character.Data.SpeedLimit * speedPercent, minSpeed);
        character.CharacterController.SimpleMove(speed * character.transform.forward);
        // 改变朝向
        float angle = Mathf.Atan2(character.Data.Vertical, character.Data.Horizontal) * Mathf.Rad2Deg;
        Rotate(angle);
    }

    /// <summary>
    /// 旋转
    /// </summary>
    /// <param name="angle">旋转角度</param>
    public void Rotate(float angle)
    {
        Vector3 originAngle = character.transform.localEulerAngles;
        originAngle.y = -angle + 90;
        character.transform.localEulerAngles = originAngle;
    }

    /// <summary>
    /// 攻击
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
            //Debug.Log($"{character.name}发动攻击，目标{hit.transform.name}");
            NPCBaseCharacter enemy = hit.transform.GetComponent<NPCBaseCharacter>();
            enemy.GetHit(character.transform, character.Data.AttackDamage);
            character.Data.AttackCombo++;
            character.Data.AttackCombo %= 2;
        }
        else
        {
            //Debug.Log($"{character.name}发动攻击，但是落空了！");
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
        //Debug.Log($"{character.name}二段攻击，击中{colliderArr.Length}个");
        character.Data.AttackCombo = 0;
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public void GetHit()
    {
        if (character.Data.IsInvincible)
        {
            return;
        }
        //Debug.Log($"{character.name}受到{character.Data.TakeDamage}伤害");
        character.Data.TimerInvincible = character.Data.InvincibleTime;
        character.Data.Health = Mathf.Max(0, character.Data.Health - character.Data.TakeDamage);
        character.Data.TakeDamage = 0;
    }

}
