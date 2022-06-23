using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMono<UIManager>
{
    private Transform mainCanvas;
    private Transform worldCanvas;

    public Transform MainCanvas { get { return mainCanvas; } }
    public Transform WorldCanvas { get { return worldCanvas; } }

    private Dictionary<string, Dictionary<string, Transform>> uiBasePanelDic;

    protected override void Awake()
    {
        base.Awake();
        uiBasePanelDic = new Dictionary<string, Dictionary<string, Transform>>();
        mainCanvas = GameObject.FindWithTag(StringTag.Main_Canvas).transform;
        worldCanvas = GameObject.FindWithTag(StringTag.World_Canvas).transform;
    }

    /// <summary>
    /// ע��ؼ�
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="widgetName"></param>
    /// <param name="widget"></param>
    public void RegistWidget(string panelName, string widgetName, Transform widget)
    {
        //Debug.Log($"����ע��{panelName} - {widgetName} - {widget.name}");
        if (!uiBasePanelDic.ContainsKey(panelName))
        {
            //Debug.Log($"������");
            uiBasePanelDic[panelName] = new();
        }
        else
        {
            //Debug.Log($"�����ֵ��д���");
        }
        uiBasePanelDic[panelName].Add(widgetName, widget);
    }

    /// <summary>
    /// ע���ؼ�
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="widgetName"></param>
    public void UnRegistWidget(string panelName, string widgetName)
    {
        if (uiBasePanelDic.ContainsKey(panelName))
        {
            if (uiBasePanelDic[panelName].ContainsKey(widgetName))
            {
                uiBasePanelDic[panelName].Remove(widgetName);
            }
        }
    }

    /// <summary>
    /// ע������Panel������Ŀؼ�
    /// </summary>
    /// <param name="panelName"></param>
    public void UnRegistPanel(string panelName)
    {
        if (uiBasePanelDic.ContainsKey(panelName))
        {
            uiBasePanelDic[panelName].Clear();
            uiBasePanelDic[panelName] = null;
            uiBasePanelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// ��ȡ�ؼ�
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="widgetName"></param>
    /// <returns></returns>
    public Transform GetWidget(string panelName, string widgetName)
    {
        if (uiBasePanelDic.ContainsKey(panelName))
        {
            return uiBasePanelDic[panelName][widgetName];
        }
        return null;
    }

    /// <summary>
    /// ����Panel
    /// </summary>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public Transform LoadPanel(string resPath)
    {
        Transform obj = Resources.Load<Transform>(resPath);
        if (obj != null)
        {
            Transform panel = Instantiate(obj);
            panel.name = panel.name.Replace("(Clone)", "");
            panel.SetParent(MainCanvas, false);
            return panel;
        }
        return null;
    }

}
