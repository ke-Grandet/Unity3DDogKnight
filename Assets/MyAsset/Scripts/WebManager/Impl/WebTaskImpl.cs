using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTaskImpl : WebBaseTask
{

    public WebTaskImpl(string url)
    {
        Init(url);
    }

    public void Init(string url)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Url = "file:///" + url;
        } 
        else if (Application.platform == RuntimePlatform.Android)
        {
            Url = "jar:file://" + url;
        }
        else
        {
            Url = "file://" + url;
        }
    }

    protected override void StartDownload()
    {
        base.StartDownload();
        Debug.Log("开启下载");
    }

    protected override void DownloadSuccess(DownloadHandler response)
    {
        base.DownloadSuccess(response);
        Debug.Log("下载完成：" + response.text);
    }

    protected override void DownloadFail(WebBaseTask webTask)
    {
        base.DownloadFail(webTask);
        WebManager.Instance.AddTask(webTask);
    }

}
