using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GruntCharacter : NPCBaseCharacter
{
    private GruntSimpleData data;
    private GruntLogic logic;
    private readonly Collider[] colliderArr = new Collider[5];  // ���߼�����ײ�建������

    public GruntSimpleData Data { get { return data; } }
    public GruntLogic Logic { get { return logic; } }

    protected override void Awake()
    {
        base.Awake();
        logic = new(this);
        fsm.AddState(StateEnum.IDLE, new GruntIdleState(this));
        fsm.AddState(StateEnum.WALK, new GruntWalkState(this));
        fsm.AddState(StateEnum.ATTACK, new GruntAttackState(this));
        fsm.AddState(StateEnum.GET_HIT, new GruntGetHitState(this));
        fsm.AddState(StateEnum.DIE, new GruntDieState(this));
    }

    public override void Initial()
    {
        gameObject.SetActive(true);
        data = ResourceManager.Instance.FindResourceFromJson<GruntSimpleData>(StringDataPath.Grunt_Simple_Data);
        fsm.ChangeState(StateEnum.IDLE);
    }

    public override void GetHit(Transform attacker, int damage)
    {
        if (fsm.CurrentState != StateEnum.DIE)
        {
            data.TakeDamage += damage;
            logic.GetHit();
            if (fsm.CurrentState != StateEnum.GET_HIT)
            {
                fsm.ChangeState(StateEnum.GET_HIT);
            }
        }
    }

    protected override void Update()
    {
        if (isControlled)
        {
            return;
        }
        base.Update();
        // ����Ѫ����ʣ��Ѫ������ʾλ��
        UpdateHealthBar(data.Health, data.MaxHealth);
        // �������
        if (data.Health <= 0 && fsm.CurrentState != StateEnum.DIE)
        {
            fsm.ChangeState(StateEnum.DIE);
        }
        // ��⾯�䷶Χ�Ƿ�����ҽ�ɫ���ǵĻ���Ŀ������Ϊ���������һ��
        int playerCount = Physics.OverlapSphereNonAlloc(transform.position, data.WarnRange, colliderArr, StringLayerName.LayerMask_Player);
        if (playerCount > 0)
        {
            if (target == null || target.gameObject.layer != StringLayerName.Layer_Player)
            {
                target = colliderArr[0].transform;
            }
            for (int i = 0; i < playerCount; i++)
            {
                if (Vector3.Distance(colliderArr[i].transform.position, transform.position) < DistanceToTarget.magnitude)
                {
                    target = colliderArr[i].transform;
                }
            }
        }
        else
        {
            target = defaultTarget;
        }
        fsm.OnUpdate();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, data.WarnRange);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, att);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
