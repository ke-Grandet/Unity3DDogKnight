using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Resources资源管理器，已获取过的资源存放于缓存中
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    private readonly Dictionary<string, Object> resourceDic;

    private ResourceManager()
    {
        resourceDic = new();
    }

    /// <summary>
    /// 获取Resources目录下的资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="resourceName">资源路径</param>
    /// <returns>资源对象</returns>
    public T FindResource<T> (string resourceName) where T : Object
    {
        if (!resourceDic.TryGetValue(resourceName, out Object resource))
        {
            resource = Resources.Load(resourceName);
            if (resource == null)
            {
                Debug.Log("没有找到资源：" + resourceName);
            }
            resourceDic.Add(resourceName, resource);
        }
        return resource as T;
    }

    /// <summary>
    /// 获取Resources目录下的资源，对象类型默认为GameObject
    /// </summary>
    /// <param name="resourceName">资源路径</param>
    /// <returns>资源对象</returns>
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
                Debug.Log("没有找到资源：" + resourceName);
            }
            resourceDic.Add(resourceName, resource);
        }
        return Object.Instantiate( resource as Sprite);
    }

    /// <summary>
    /// 获取Resources目录下的JSON文本
    /// </summary>
    /// <typeparam name="T">JSON文本的结构所对应的数据类</typeparam>
    /// <param name="resourceName">资源路径</param>
    /// <returns>资源对象</returns>
    public T FindResourceFromJson<T> (string resourceName) where T : class
    {
        TextAsset textAsset = FindResource<TextAsset>(resourceName);
        //Debug.Log($"读取到配置: \n{textAsset.text}");
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}