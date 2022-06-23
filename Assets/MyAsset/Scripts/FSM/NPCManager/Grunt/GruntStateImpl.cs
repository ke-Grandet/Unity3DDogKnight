using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntIdleState : BaseState<GruntCharacter>
{
    public GruntIdleState(GruntCharacter controller) : base(controller) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("进入IDLE");
        character.Animator.Play(AnimatorStringHash.idle);
    }
    public override void OnUpdate()
    {
        if (character.Target != null)
        {
            if (character.DistanceToTarget.magnitude <= character.Data.AttackDistance)
            {
                character.FSM.ChangeState(StateEnum.ATTACK);
            }
            else
            {
                character.FSM.ChangeState(StateEnum.WALK);
            }
        }
    }
    public override void OnExit()
    {
        //Debug.Log("离开IDLE");
    }
}


public class GruntWalkState : BaseState<GruntCharacter>
{
    public GruntWalkState(GruntCharacter controller) : base(controller) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("进入WALK");
        character.Animator.Play(AnimatorStringHash.walk);
    }
    public override void OnUpdate()
    {
        if (character.Target != null)
        {
            if (character.DistanceToTarget.magnitude <= character.Data.AttackDistance)
            {
                character.FSM.ChangeState(StateEnum.ATTACK);
            }
            else
            {
                character.transform.LookAt(character.Target);
                character.CharacterController.SimpleMove(character.DistanceToTarget.normalized * character.Data.MoveSpeed);
            }
        }
        else
        {
            character.FSM.ChangeState(StateEnum.IDLE);
        }
    }
    public override void OnExit()
    {
        //Debug.Log("离开WALK");
    }
}


public class GruntAttackState : BaseState<GruntCharacter>
{
    private bool isDidAttack;
    private float timerForeswing;
    public GruntAttackState(GruntCharacter controller) : base(controller) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("进入ATTACK");
        character.Animator.Play(AnimatorStringHash.idle);
        isDidAttack = false;
        timerForeswing = 0.5f;
        character.transform.LookAt(character.Target);
    }
    public override void OnUpdate()
    {
        if (timerForeswing > 0)
        {
            // 攻击前摇
            timerForeswing = Mathf.Max(0f, timerForeswing - Time.deltaTime);
            if (timerForeswing == 0f)
            {
                character.Animator.Play(AnimatorStringHash.attack);
            }
            return;
        }
        if (!isDidAttack && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f)
        {
            isDidAttack = true;
            AudioManager.Instance.PlayOneShot(StringAudioName.Axe_Attack);
            character.Logic.Attack();
        }
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            if (character.DistanceToTarget.magnitude <= character.Data.AttackDistance)
            {
                character.FSM.ChangeState(StateEnum.ATTACK);
            }
            else
            {
                character.FSM.ChangeState(StateEnum.IDLE);
            }
        }
    }
    public override void OnExit()
    {
        //Debug.Log("离开ATTACK");
    }
}


public class GruntGetHitState : BaseState<GruntCharacter>
{
    public GruntGetHitState(GruntCharacter controller) : base(controller) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("进入GET_HIT");
        character.Animator.Play(AnimatorStringHash.getHit);
    }
    public override void OnUpdate()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            if (character.DistanceToTarget.magnitude <= character.Data.AttackDistance)
            {
                character.FSM.ChangeState(StateEnum.ATTACK);
            }
            else
            {
                character.FSM.ChangeState(StateEnum.IDLE);
            }
        }
    }
    public override void OnExit()
    {
        //Debug.Log("离开GET_HIT");
    }
}


public class GruntDieState : BaseState<GruntCharacter>
{
    private float timerLast;
    public GruntDieState(GruntCharacter controller) : base(controller) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log($"{character.name}进入DIE");
        character.Animator.Play(AnimatorStringHash.die);
        timerLast = 1f;
    }
    public override void OnUpdate()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            timerLast -= Time.deltaTime;
            if (timerLast <= 0f)
            {
                NPCManager.Instance.ReturnNPC(character);
            }
        }
    }
    public override void OnExit()
    {
        //Debug.Log("离开DIE");
    }
}

