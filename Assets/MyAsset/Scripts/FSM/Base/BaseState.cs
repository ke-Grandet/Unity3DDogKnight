using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T> : IState
{
    protected T character;
    protected StateEnum preState;

    public BaseState(T character)
    {
        this.character = character;
    }

    public virtual void OnEnter(StateEnum preState) 
    {
        this.preState = preState;
    }
    public abstract void OnUpdate();
    public abstract void OnExit();
}
