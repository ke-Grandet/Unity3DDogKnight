using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DogKnightIdleState : BaseState<DogKnightCharacter>
{
    public DogKnightIdleState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("����idle״̬");
        character.Animator.Play(AnimatorStringHash.idle);
    }
    public override void OnUpdate()
    {
        if (character.FSM.NextState > StateEnum.IDLE)
        {
            character.FSM.ChangeState(character.FSM.NextState);
        }
        else if (character.Data.IsMove)
        {
            character.FSM.ChangeState(
                character.Data.SpeedLimit <= character.Data.WalkSpeed ? StateEnum.WALK : StateEnum.RUN);
        }
    }
    public override void OnExit()
    {
        //Debug.Log("�뿪idle״̬");
    }
}


public class DogKnightWalkState : BaseState<DogKnightCharacter>
{
    public DogKnightWalkState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("����walk״̬");
        character.Animator.Play(AnimatorStringHash.walk);
        character.Data.SpeedLimit = Mathf.Min(character.Data.SpeedLimit, character.Data.WalkSpeed);
        character.Data.NotifyUI();
    }
    public override void OnUpdate()
    {
        if (character.FSM.NextState != StateEnum.IDLE)
        {
            character.FSM.ChangeState(character.FSM.NextState);
        }
        else if (character.Data.IsMove)
        {
            if (character.Data.SpeedLimit > character.Data.WalkSpeed)
            {
                character.FSM.ChangeState(StateEnum.RUN);
            }
            else
            {
                character.Logic.Move();
            }
        }
        else
        {
            character.FSM.ChangeState(StateEnum.IDLE);
        }
    }
    public override void OnExit()
    {
        //Debug.Log("�뿪walk״̬");
    }
}


public class DogKnightRunState : BaseState<DogKnightCharacter>
{
    public DogKnightRunState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("����run״̬");
        character.Animator.Play(AnimatorStringHash.run);
        character.Data.SpeedLimit = Mathf.Max(character.Data.SpeedLimit, character.Data.RunSpeed);
        character.Data.NotifyUI();
    }
    public override void OnUpdate()
    {
        if (character.FSM.NextState != StateEnum.IDLE)
        {
            character.FSM.ChangeState(character.FSM.NextState);
        }
        else if (character.Data.IsMove)
        {
            if (character.Data.SpeedLimit <= character.Data.WalkSpeed)
            {
                character.FSM.ChangeState(StateEnum.WALK);
            }
            else
            {
                character.Logic.Move(character.Data.WalkSpeed);  // �ƶ�ʱ������С�ٶȲ�����Walk�ٶ�
            }
        }
        else
        {
            character.FSM.ChangeState(StateEnum.IDLE);
        }
    }
    public override void OnExit()
    {
        //Debug.Log("�뿪run״̬");
    }
}


public class DogKnightAttackState : BaseState<DogKnightCharacter>
{
    private bool isDidAttack;
    private float attackEffectTime;
    private UnityAction attackFunction;
    public DogKnightAttackState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log($"{character.name}����attack״̬");
        if (character.Data.AttackCombo == 0)
        {
            character.Animator.Play(AnimatorStringHash.attack, 0, 0f);
            attackEffectTime = 0.5f;
            attackFunction = character.Logic.Attack;
        }
        else
        {
            character.Animator.Play(AnimatorStringHash.attackSecond, 0, 0f);
            attackEffectTime = 0.4f;
            attackFunction = character.Logic.AttackSecond;
        }
        isDidAttack = false;
    }
    public override void OnUpdate()
    {
        if (character.FSM.NextState == StateEnum.GET_HIT)
        {
            character.FSM.ChangeState(character.FSM.NextState);
        }
        else
        {
            if (!isDidAttack && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackEffectTime)
            {
                isDidAttack = true;
                AudioManager.Instance.PlayOneShot(StringAudioName.Sword_Attack);
                attackFunction();
            }
            if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
            {
                if (character.FSM.NextState == StateEnum.ATTACK && !character.Data.IsMove)
                {
                    character.FSM.ChangeState(character.FSM.NextState);
                }
                else if (preState == StateEnum.WALK || preState == StateEnum.RUN)
                {
                    character.FSM.ChangeState(preState);
                    return;
                }
                else
                {
                    character.FSM.ChangeState(StateEnum.IDLE);
                    return;
                }
            }
        }
    }
    public override void OnExit()
    {
        if (character.FSM.NextState != StateEnum.ATTACK)
        {
            character.Data.AttackCombo = 0;
        }
        //Debug.Log("�뿪attack״̬");
    }
}


public class DogKnightGetHitState : BaseState<DogKnightCharacter>
{
    public DogKnightGetHitState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log("����getHit״̬");
        character.Animator.Play(AnimatorStringHash.getHit);
        character.Logic.GetHit();
        character.Data.NotifyUI();
    }
    public override void OnUpdate()
    {
        if (character.FSM.NextState == StateEnum.DIE)
        {
            character.FSM.ChangeState(character.FSM.NextState);
        }
        else if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            if (character.FSM.NextState != StateEnum.IDLE)
            {
                character.FSM.ChangeState(character.FSM.NextState);
            }
            else if (preState == StateEnum.WALK || preState == StateEnum.RUN)
            {
                character.FSM.ChangeState(preState);
            }
            else
            {
                character.FSM.ChangeState(StateEnum.IDLE);
            }
        }
    }
    public override void OnExit()
    {
        character.Data.NotifyUI();
        //Debug.Log("�뿪getHit״̬");
    }
}


public class DogKnightDieState : BaseState<DogKnightCharacter>
{
    public DogKnightDieState(DogKnightCharacter character) : base(character) { }
    public override void OnEnter(StateEnum preState)
    {
        base.OnEnter(preState);
        //Debug.Log($"{character.name}����die״̬");
        character.Animator.Play(AnimatorStringHash.die);
    }
    public override void OnUpdate()
    {
        if (preState != StateEnum.DIE && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            PlayerManager.Instance.GameOver();
        }
    }
    public override void OnExit()
    {
        //Debug.Log("�뿪die״̬");
    }
}