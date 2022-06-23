using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AoeCheck
{
    /// <summary>
    /// 矩形攻击判定
    /// </summary>
    /// <param name="attacker">攻击者</param>
    /// <param name="target">攻击目标</param>
    /// <param name="forwardRange">攻击的前方范围，即从攻击者位置到正前方可攻击到位置的最远距离</param>
    /// <param name="sideRange">攻击的侧方范围，即从攻击者位置到任一正侧方可攻击到位置的最远距离</param>
    /// <returns>目标在攻击范围内返回true，否则返回false</returns>
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
    /// 扇形攻击判定
    /// </summary>
    /// <param name="attacker">攻击者</param>
    /// <param name="target">攻击目标</param>
    /// <param name="radius">攻击半径</param>
    /// <param name="angle">攻击角度</param>
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
