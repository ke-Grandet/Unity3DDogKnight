using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DogKnightCharacter : BaseCharacter
{
    private DogKnightSimpleData data = new();
    private DogKnightLogic logic;
    private SkillData skillData;
    private SkillLogic skillLogic;

    public DogKnightSimpleData Data { get { return data; } }
    public DogKnightLogic Logic { get { return logic; } }
    public SkillData SkillData { get { return skillData; } }
    public SkillLogic SkillLogic { get { return skillLogic; } }

    private void OnAnimatorMove()
    {
        //characterController.Move(animator.deltaPosition);
        //characterController.Move(Vector3.forward * 0.1f);
        // ��ȡģ�����ֵı任����
        //animator.GetBoneTransform(HumanBodyBones.RightHand);
    }

    protected override void Awake()
    {
        base.Awake();
        logic = new(this);
        skillLogic = new(this);
        fsm.AddState(StateEnum.IDLE, new DogKnightIdleState(this));
        fsm.AddState(StateEnum.WALK, new DogKnightWalkState(this));
        fsm.AddState(StateEnum.RUN, new DogKnightRunState(this));
        fsm.AddState(StateEnum.ATTACK, new DogKnightAttackState(this));
        fsm.AddState(StateEnum.GET_HIT, new DogKnightGetHitState(this));
        fsm.AddState(StateEnum.DIE, new DogKnightDieState(this));
        data = ResourceManager.Instance.FindResourceFromJson<DogKnightSimpleData>(StringDataPath.DogKnight_Simple_Data);
        Initial();
    }

    private void Start()
    {
        skillData = new();
    }

    public void Initial()
    {
        data.Health = data.MaxHealth;
        data.SpeedLimit = data.WalkSpeed;
        data.TakeDamage = 0;
        data.TimerInvincible = 0;
        data.AttackCombo = 0;
        data.NotifyUI();
        fsm.ChangeState(StateEnum.IDLE);
    }

    /// <summary>
    /// �����ƶ��������
    /// </summary>
    /// <param name="horizontal">��������</param>
    /// <param name="vertical">��������</param>
    public void InputMove(float horizontal, float vertical)
    {
        data.Horizontal = horizontal;
        data.Vertical = vertical;
    }

    /// <summary>
    /// ����״ָ̬�������
    /// </summary>
    /// <param name="nextState">��һ��״̬</param>
    public void CommandState(StateEnum nextState)
    {
        fsm.NextState = nextState;
        fsm.TimerStateCommandRemain = fsm.StateCommandRemainTime;
    }

    public override void GetHit(Transform attacker, int damage)
    {
        if (!data.IsInvincible)
        {
            data.TakeDamage += damage;
            CommandState(StateEnum.GET_HIT);
        }
    }

    void Update()
    {
        Debug.DrawRay(transform.position + transform.up, transform.forward * Data.AttackDistance, Color.red);
        // ִ���������ڲ��ĵ���ʱ�߼�
        data.OnUpdate();
        // ִ�м����������ڲ��ĵ���ʱ�߼�
        skillData.OnUpdate();
        // ִ��״̬���ڲ��߼�
        fsm.OnUpdate();
        // �������
        if (data.Health <= 0 && fsm.CurrentState != StateEnum.DIE)
        {
            fsm.ChangeState(StateEnum.DIE);
        }
        // ���������ƶ�
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (fsm.CurrentState == StateEnum.WALK)
            {
                CommandState(StateEnum.RUN);
            }
            else if (fsm.CurrentState == StateEnum.RUN)
            {
                CommandState(StateEnum.WALK);
            }
        }
        // ��������
        if (Input.GetKeyDown(KeyCode.A))
        {
            CommandState(StateEnum.ATTACK);
        }
        // �������򣺱�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillLogic.Summon(ElementEnum.QUAS);
            skillData.NotifyUI();
        }
        // ����������
        if (Input.GetKeyDown(KeyCode.W))
        {
            skillLogic.Summon(ElementEnum.WEX);
            skillData.NotifyUI();
        }
        // �������򣺻�
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillLogic.Summon(ElementEnum.EXORT);
            skillData.NotifyUI();
        }
        // �����м���
        if (Input.GetKeyDown(KeyCode.R))
        {
            skillLogic.Invoke();
            skillData.NotifyUI();
        }
        // �����ż��ܣ�D
        if (Input.GetKeyDown(KeyCode.D))
        {
            skillLogic.ReleaseFirstSkill();
            skillData.NotifyUI();
        }
        // �����ż��ܣ�F
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillLogic.ReleaseSecondSkill();
            skillData.NotifyUI();
        }
        // ��ʱ����������
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    GetHit(transform, 10);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, data.AttackDistance);
    }
}
