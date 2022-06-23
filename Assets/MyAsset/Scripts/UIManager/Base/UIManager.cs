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
    /// 注册控件
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="widgetName"></param>
    /// <param name="widget"></param>
    public void RegistWidget(string panelName, string widgetName, Transform widget)
    {
        //Debug.Log($"正在注册{panelName} - {widgetName} - {widget.name}");
        if (!uiBasePanelDic.ContainsKey(panelName))
        {
            //Debug.Log($"不存在");
            uiBasePanelDic[panelName] = new();
        }
        else
        {
            //Debug.Log($"已在字典中存在");
        }
        uiBasePanelDic[panelName].Add(widgetName, widget);
    }

    /// <summary>
    /// 注销控件
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
    /// 注销整个Panel和下面的控件
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
    /// 获取控件
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
    /// 加载Panel
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
