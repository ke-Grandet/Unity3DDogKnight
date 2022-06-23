using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AoeCheck
{
    /// <summary>
    /// ���ι����ж�
    /// </summary>
    /// <param name="attacker">������</param>
    /// <param name="target">����Ŀ��</param>
    /// <param name="forwardRange">������ǰ����Χ�����ӹ�����λ�õ���ǰ���ɹ�����λ�õ���Զ����</param>
    /// <param name="sideRange">�����Ĳ෽��Χ�����ӹ�����λ�õ���һ���෽�ɹ�����λ�õ���Զ����</param>
    /// <returns>Ŀ���ڹ�����Χ�ڷ���true�����򷵻�false</returns>
    public static bool RectAttack(Transform attacker, Transform target, float forwardRange, float sideRange)
    {
        Vector3 attackToTarget = target.position - attacker.position;
        float forwardDistance = Vector3.Dot(attacker.forward, attackToTarget);
        if (forwardDistance > forwardRange || forwardDistance < 0f)
        {
            return false;
        }
        float sideDistance = Vector3.Dot(attacker.right, attackToTarget);
        if (Mathf.Abs(sideDistance) > sideRange)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// ���ι����ж�
    /// </summary>
    /// <param name="attacker">������</param>
    /// <param name="target">����Ŀ��</param>
    /// <param name="radius">�����뾶</param>
    /// <param name="angle">�����Ƕ�</param>
    /// <returns></returns>
    public static bool SectorAttack(Transform attacker, Transform target, float radius, float angle)
    {
        Vector3 attackToTarget = target.position - attacker.position;
        if (attackToTarget.magnitude > radius)
        {
            return false;
        }
        float cosValue = Vector3.Dot(attacker.forward, attackToTarget.normalized);
        if (Mathf.Acos(cosValue) * Mathf.Rad2Deg > angle * 0.5f)
        {
            return false;
        }
        return true;
    }
}
