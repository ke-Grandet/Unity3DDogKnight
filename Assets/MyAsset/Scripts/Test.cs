using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Model
{
    public SceneModel[] SceneArr;
}
[System.Serializable]
public class SceneModel
{
    public string SceneName;
    public PanelModel[] PanelArr;
}
[System.Serializable]
public class PanelModel
{
    public string PanelName;
    public string PanelPath;
}

public class Test : MonoBehaviour
{

    void Start()
    {
        Model model = ResourceManager.Instance.FindResourceFromJson<Model>(StringConfigPath.UI_Panel_Config);
    }

    void Update()
    {

    }

    private void LateUpdate()
    {

    }
}
