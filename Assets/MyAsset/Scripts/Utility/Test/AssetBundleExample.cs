using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetBundleExample : MonoBehaviour
{
    public string path = "Assets/AssetBundles/scenebundle.lab";

    void Start()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(path);
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("加载AssetBundle失败");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Red");
        GameObject red = Instantiate(prefab);
        red.transform.position = Vector3.zero;

        //StartCoroutine(InstantiateObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 如果是您自己托管 AssetBundle 并需要将它们下载到应用程序中，请使用 UnityWebRequestAssetBundle API。
    // 下面是一个示例：
    private string assetBundleName = "scenebundle.lab";
    IEnumerator InstantiateObject()
    {
        string url = "file:///" + Application.dataPath + "/AssetBundles/" + assetBundleName;
        var request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
        yield return request.SendWebRequest();
        AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
        GameObject cube = bundle.LoadAsset<GameObject>("Red");
        GameObject red = Instantiate(cube);
        red.transform.position = Vector3.zero + Vector3.up * 5;
    }
}
