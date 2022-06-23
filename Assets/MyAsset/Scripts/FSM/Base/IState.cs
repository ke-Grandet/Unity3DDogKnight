using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(StateEnum preState);
    public void OnUpdate();
    public void OnExit();
}
