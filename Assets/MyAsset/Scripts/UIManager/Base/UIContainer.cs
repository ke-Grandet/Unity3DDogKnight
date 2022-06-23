using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIContainer : MonoBehaviour
{

    private Dictionary<string, Transform> widgetDic;

    protected virtual void Awake()
    {
        UIBasePanel panel = GetComponentInParent<UIBasePanel>();
        UIManager.Instance.RegistWidget(panel.name, name, transform);

        widgetDic = new();
        Transform[] transformArr = GetComponentsInChildren<Transform>();
        foreach (Transform t in transformArr)
        {
            if (t.name.EndsWith("_S"))
            {
                widgetDic.Add(t.name, t);
            }
        }
    }

    /// <summary>
    /// 获取子控件上特定类型的组件
    /// </summary>
    /// <param name="widgetName"></param>
    /// <returns></returns>
    public T GetWidget<T>(string widgetName) where T : Component
    {
        if (widgetDic[widgetName] != null)
        {
            return widgetDic[widgetName].GetComponent<T>();
        }
        return null;
    }

    protected virtual void OnDestroy()
    {
        if (widgetDic != null)
        {
            widgetDic.Clear();
            widgetDic = null;
        }
    }

}
