using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntLogic
{
    private readonly GruntCharacter character;

    public GruntLogic(GruntCharacter character)
    {
        this.character = character;
    }

    public void Attack()
    {
        if (Physics.Raycast(
                new(character.transform.position + character.transform.up, character.transform.forward),
                out RaycastHit hit,
                character.Data.AttackDistance,
                StringLayerName.LayerMask_Player | StringLayerName.LayerMask_Holy
            ))
        {
            BaseCharacter enemy = hit.transform.GetComponent<BaseCharacter>();
            enemy.GetHit(character.transform, character.Data.AttackDamage);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void GetHit()
    {
        character.Data.Health = Mathf.Max(0, character.Data.Health - character.Data.TakeDamage);
        //Debug.Log($"�ܵ��˺�{character.Data.TakeDamage}������ʣ������ֵ{character.Data.Health}");
        character.Data.TakeDamage = 0;
    }
}
