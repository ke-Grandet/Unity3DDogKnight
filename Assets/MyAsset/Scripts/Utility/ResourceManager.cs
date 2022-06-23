using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Resources��Դ���������ѻ�ȡ������Դ����ڻ�����
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    private readonly Dictionary<string, Object> resourceDic;

    private ResourceManager()
    {
        resourceDic = new();
    }

    /// <summary>
    /// ��ȡResourcesĿ¼�µ���Դ
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="resourceName">��Դ·��</param>
    /// <returns>��Դ����</returns>
    public T FindResource<T> (string resourceName) where T : Object
    {
        if (!resourceDic.TryGetValue(resourceName, out Object resource))
        {
            resource = Resources.Load(resourceName);
            if (resource == null)
            {
                Debug.Log("û���ҵ���Դ��" + resourceName);
            }
            resourceDic.Add(resourceName, resource);
        }
        return resource as T;
    }

    /// <summary>
    /// ��ȡResourcesĿ¼�µ���Դ����������Ĭ��ΪGameObject
    /// </summary>
    /// <param name="resourceName">��Դ·��</param>
    /// <returns>��Դ����</returns>
    public GameObject FindResource(string resourceName)
    {
        return FindResource<GameObject>(resourceName);
    }

    public Sprite FindSprite(string resourceName)
    {
        if (!resourceDic.TryGetValue(resourceName, out Object resource))
        {
            resource = Resources.Load<Sprite>(resourceName);
            if (resource == null)
            {
                Debug.Log("û���ҵ���Դ��" + resourceName);
            }
            resourceDic.Add(resourceName, resource);
        }
        return Object.Instantiate( resource as Sprite);
    }

    /// <summary>
    /// ��ȡResourcesĿ¼�µ�JSON�ı�
    /// </summary>
    /// <typeparam name="T">JSON�ı��Ľṹ����Ӧ��������</typeparam>
    /// <param name="resourceName">��Դ·��</param>
    /// <returns>��Դ����</returns>
    public T FindResourceFromJson<T> (string resourceName) where T : class
    {
        TextAsset textAsset = FindResource<TextAsset>(resourceName);
        //Debug.Log($"��ȡ������: \n{textAsset.text}");
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}