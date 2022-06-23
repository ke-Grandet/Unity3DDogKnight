using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBasePanel : MonoBehaviour
{

    protected virtual void Awake()
    {
        Transform[] transformArr = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transformArr.Length; i++)
        {
            if (transformArr[i].name.EndsWith("_N"))
            {
                UIManager.Instance.RegistWidget(name, transformArr[i].name, transformArr[i]);
            }
            if (transformArr[i].name.EndsWith("_C"))
            {
                transformArr[i].gameObject.AddComponent<UIContainer>();
            }
        }
    }

    public Transform GetCanvas()
    {
        return UIManager.Instance.MainCanvas;
    }

    /// <summary>
    /// ��ȡһ���ӿؼ����ض����͵����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="widgetName"></param>
    /// <returns></returns>
    public T GetWidget<T> (string widgetName) where T : Component
    {
        Transform widget = UIManager.Instance.GetWidget(name, widgetName);
        if (widget != null)
        {
            return widget.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// ��ȡ�����ӿؼ����ض����͵����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cellName"></param>
    /// <param name="widgetName"></param>
    /// <returns></returns>
    public T GetWidget<T> (string cellName, string widgetName) where T : Component
    {
        Transform cell = UIManager.Instance.GetWidget(name, cellName);
        if (cell != null)
        {
            UIContainer uiSubManager = cell.GetComponent<UIContainer>();
            if (uiSubManager != null)
            {
                return uiSubManager.GetWidget<T>(widgetName);
            }
        }
        return null;
    }

    /// <summary>
    /// ���ӿؼ�������¼��ӿ�
    /// </summary>
    /// <param name="widgetName"></param>
    /// <param name="type"></param>
    /// <param name="action"></param>
    public void AddEventToWidget(string widgetName, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        Transform widget = UIManager.Instance.GetWidget(name, widgetName);
        AddEventToWidget(widget, type, action);
    }
    public void AddEventToWidget(Component widget, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        // ��ȡ�¼���������û�������
        EventTrigger eventTrigger = widget.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = widget.gameObject.AddComponent<EventTrigger>();
        }
        // ��ʼ��һ���¼�ʵ�壬��������¼������ʵ��Ļص�
        EventTrigger.Entry entry = new();
        entry.eventID = type;
        entry.callback = new();
        entry.callback.AddListener(action);
        // ���¼�ʵ����뵽�¼���������
        eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void HideSelf()
    {
        GetWidget<Transform>("Root_N").gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        UIManager.Instance.UnRegistPanel(name);
    }

}
