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
        // 获取模型右手的变换对象
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
    /// 接收移动轴的输入
    /// </summary>
    /// <param name="horizontal">横轴输入</param>
    /// <param name="vertical">纵轴输入</param>
    public void InputMove(float horizontal, float vertical)
    {
        data.Horizontal = horizontal;
        data.Vertical = vertical;
    }

    /// <summary>
    /// 接收状态指令的输入
    /// </summary>
    /// <param name="nextState">下一个状态</param>
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
        // 执行数据类内部的倒计时逻辑
        data.OnUpdate();
        // 执行技能数据类内部的倒计时逻辑
        skillData.OnUpdate();
        // 执行状态机内部逻辑
        fsm.OnUpdate();
        // 检测死亡
        if (data.Health <= 0 && fsm.CurrentState != StateEnum.DIE)
        {
            fsm.ChangeState(StateEnum.DIE);
        }
        // 按键加速移动
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
        // 按键攻击
        if (Input.GetKeyDown(KeyCode.A))
        {
            CommandState(StateEnum.ATTACK);
        }
        // 按键切球：冰
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillLogic.Summon(ElementEnum.QUAS);
            skillData.NotifyUI();
        }
        // 按键切球：雷
        if (Input.GetKeyDown(KeyCode.W))
        {
            skillLogic.Summon(ElementEnum.WEX);
            skillData.NotifyUI();
        }
        // 按键切球：火
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillLogic.Summon(ElementEnum.EXORT);
            skillData.NotifyUI();
        }
        // 按键切技能
        if (Input.GetKeyDown(KeyCode.R))
        {
            skillLogic.Invoke();
            skillData.NotifyUI();
        }
        // 按键放技能：D
        if (Input.GetKeyDown(KeyCode.D))
        {
            skillLogic.ReleaseFirstSkill();
            skillData.NotifyUI();
        }
        // 按键放技能：F
        if (Input.GetKeyDown(KeyCode.F))
        {
            skillLogic.ReleaseSecondSkill();
            skillData.NotifyUI();
        }
        // 临时：按键受伤
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
