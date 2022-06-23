using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private readonly IState[] stateArr;
    private int loadCount = 0;  // ��װ��״̬������
    private StateEnum stateIndex = StateEnum.NULL;  // ��ǰ״̬������
    // ״ָ̬�����
    private StateEnum nextState;  // ��һ״̬
    private float stateCommandRemainTime = 1.2f;  // ״ָ̬���ݴ�ʱ��
    private float timerStateCommandRemain = 0f;  // ״ָ̬���ݴ��ʱ��

    public StateEnum CurrentState { get { return stateIndex; } }
    public StateEnum NextState { get { return nextState; } set { nextState = value; } }
    public float StateCommandRemainTime { get { return stateCommandRemainTime; } set { stateCommandRemainTime = value; } }
    public float TimerStateCommandRemain { get { return timerStateCommandRemain; } set { timerStateCommandRemain = value; } }

    public FSM(int stateCount)
    {
        stateArr = new IState[stateCount];
    }

    public void AddState(StateEnum stateEnum, IState state)
    {
        if (loadCount < stateArr.Length)
        {
            stateArr[(int)stateEnum] = state;
            loadCount++;
        }
    }

    public void ChangeState(StateEnum stateIndex)
    {
        if (stateIndex < 0 || (int)stateIndex > stateArr.Length - 1)
        {
            return;
        }
        if (this.stateIndex != StateEnum.NULL)
        {
            stateArr[(int)this.stateIndex].OnExit();
        }
        stateArr[(int)stateIndex].OnEnter(this.stateIndex);
        this.stateIndex = stateIndex;
        nextState = StateEnum.IDLE;
    }

    public void OnUpdate()
    {
        // ��ʱ���״ָ̬��
        if (timerStateCommandRemain > 0f)
        {
            timerStateCommandRemain = Mathf.Max(0f, timerStateCommandRemain - Time.deltaTime);
            if (timerStateCommandRemain == 0f)
            {
                nextState = StateEnum.IDLE;
            }
        }
        // ִ��״̬�ڲ��߼�
        if (stateIndex != StateEnum.NULL)
        {
            stateArr[(int)stateIndex].OnUpdate();
        }
    }
}
