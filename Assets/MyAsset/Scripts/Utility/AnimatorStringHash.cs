using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorStringHash
{
    public static int idle = Animator.StringToHash("Idle");
    public static int walk = Animator.StringToHash("Walk");
    public static int run = Animator.StringToHash("Run");
    public static int attack = Animator.StringToHash("Attack01");
    public static int attackSecond = Animator.StringToHash("Attack02");
    public static int getHit = Animator.StringToHash("GetHit");
    public static int die = Animator.StringToHash("Die");
    public static int victory = Animator.StringToHash("Victory");
}
