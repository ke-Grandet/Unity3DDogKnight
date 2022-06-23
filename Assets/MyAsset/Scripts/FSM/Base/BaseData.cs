using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BaseData<T> where T : class
{
    protected UnityAction<T> onValueChange;

    public void AddEvent(UnityAction<T> action)
    {
        if (onValueChange == null)
        {
            onValueChange = action;
        }
        else
        {
            onValueChange += action;
        }
    }
    public void RemoveEvent(UnityAction<T> action)
    {
        onValueChange -= action;
    }
    public void NotifyUI()
    {
        onValueChange?.Invoke(this as T);
    }
}
