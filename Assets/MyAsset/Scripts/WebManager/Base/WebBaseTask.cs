using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebBaseTask
{

    private string url;
    public string Url { get { return url; } set { url = value; } }

    protected virtual void StartDownload() { }
    protected virtual void DownloadSuccess(DownloadHandler response) { }
    protected virtual void DownloadFail(WebBaseTask webTask) { }


    public IEnumerator Download()
    {
        StartDownload();
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            DownloadSuccess(request.downloadHandler);
        }
        else
        {
            DownloadFail(this);
        }
    }

    public delegate void OnSuccess();
    public delegate void OnFail();

    private IEnumerator SendGetCoro(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        // 异常处理
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("调用结果: " + request.downloadHandler.text);
        }
    }

    private IEnumerator SendPostCoro(string url, OnSuccess onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("URL", url);
        form.AddField("name", "hello");
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST调用结果: " + request.downloadHandler.text);
            onSuccess();
        }
    }

}