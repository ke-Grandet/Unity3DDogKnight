using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public abstract class BaseCharacter : MonoBehaviour
{
    protected CharacterController characterController;
    protected Animator animator;
    protected FSM fsm;

    public CharacterController CharacterController { get { return characterController; } }
    public Animator Animator { get { return animator; } }
    public FSM FSM { get { return fsm; } }

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        fsm = new FSM((int)StateEnum.MAX);
    }

    /// <summary>
    /// ��������ʵ�����˷���
    /// </summary>
    /// <param name="attacker">�˺���Դ</param>
    /// <param name="damage">�˺�ֵ</param>

    public virtual void GetHit(Transform attacker, int damage) { }
}
