using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GruntSimpleData : BaseData<GruntSimpleData>
{
    // �ƶ����
    public float MoveSpeed = 1.5f;
    // �������
    public int Health = 50;
    public int MaxHealth = 50;
    public int TakeDamage = 0;
    // �������
    public int AttackDamage = 5;  // ������
    public float AttackDistance = 2f;  // ��������
    public float WarnRange = 8f;  // ���䷶Χ

}
