using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GruntSimpleData : BaseData<GruntSimpleData>
{
    // ÒÆ¶¯Ïà¹Ø
    public float MoveSpeed = 1.5f;
    // ÉúÃüÏà¹Ø
    public int Health = 50;
    public int MaxHealth = 50;
    public int TakeDamage = 0;
    // ¹¥»÷Ïà¹Ø
    public int AttackDamage = 5;  // ¹¥»÷Á¦
    public float AttackDistance = 2f;  // ¹¥»÷¾àÀë
    public float WarnRange = 8f;  // ¾¯½ä·¶Î§

}
