using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    private readonly List<NPCBaseCharacter> npcList;  // 场景中所有的NPC
    private readonly Dictionary<System.Type, List<NPCBaseCharacter>> npcPoolDic;  // NPC对象池，key是NPC角色类型

    public List<NPCBaseCharacter> NPCList { get { return npcList; } }

    private NPCManager()
    {
        npcList = new();
        npcPoolDic = new();
    }

    /// <summary>
    /// 从对象池中取出NPC
    /// </summary>
    /// <typeparam name="CharacterType">NPC的角色类型</typeparam>
    /// <param name="parent">NPC的父对象</param>
    /// <param name="prefabPath">NPC的预制体路径</param>
    /// <param name="useNewPrefab">是否使用给定预制体新建NPC</param>
    /// <returns>NPC对象</returns>
    public CharacterType GetNPC<CharacterType>(Vector3 position, string prefabPath, Transform defaultTarget=null, bool useNewPrefab=false) where CharacterType : NPCBaseCharacter
    {
        //Debug.Log($"从对象池中取出NPC{prefabPath}");
        // 字典中是否存在该对象池列表
        if (!npcPoolDic.TryGetValue(typeof(CharacterType), out List<NPCBaseCharacter> npcPoolList))
        {
            npcPoolList = new();
            npcPoolDic[typeof(CharacterType)] = npcPoolList;
        }
        CharacterType npc;
        // 根据参数和对象池余量判断是否新建预制体对象
        if (useNewPrefab || npcPoolList.Count == 0)
        {
            GameObject prefab = ResourceManager.Instance.FindResource(prefabPath);
            GameObject npcObj = Object.Instantiate(prefab);
            npcObj.name = npcObj.name.Replace("(Clone)", "");
            npc = npcObj.AddComponent<CharacterType>();
        }
        else
        {
            npc = npcPoolList[0] as CharacterType;
            npcPoolList.RemoveAt(0);
            RegistNPC(npc);
        }
        npc.Initial();
        npc.OriginPosition = position;
        //Debug.Log("生成位置是： " + position);
        npc.transform.position = position;
        npc.DefaultTarget = defaultTarget;
        return npc;
    }

    /// <summary>
    /// 将NPC返回对象池
    /// </summary>
    /// <typeparam name="CharacterType">NPC的具体角色类型</typeparam>
    /// <param name="npc">要被送回的NPC对象</param>
    public void ReturnNPC(NPCBaseCharacter npc)
    {
        //Debug.Log($"NPC{npc.name}被返回对象池");
        if (!npcPoolDic.TryGetValue(npc.GetType(), out List<NPCBaseCharacter> npcPoolList))
        {
            npcPoolList = new();
        }
        npc.SetHealthBarActive(false);
        npc.gameObject.SetActive(false);
        UnRegistNPC(npc);
        npcPoolList.Add(npc);
        npcPoolDic[npc.GetType()] = npcPoolList;
    }

    /// <summary>
    /// 回收场景中所有NPC
    /// </summary>
    public void ReturnAllNPC()
    {
        foreach (NPCBaseCharacter npc in npcList)
        {
            if (!npcPoolDic.TryGetValue(npc.GetType(), out List<NPCBaseCharacter> npcPoolList))
            {
                npcPoolList = new();
            }
            npc.SetHealthBarActive(false);
            npc.gameObject.SetActive(false);
            npcPoolList.Add(npc);
            npcPoolDic[npc.GetType()] = npcPoolList;
        }
        npcList.Clear();
    }

    /// <summary>
    /// 将NPC加入到管理器中
    /// </summary>
    /// <param name="npc">NPC</param>
    public void RegistNPC(NPCBaseCharacter npc)
    {
        if (!npcList.Contains(npc))
        {
            npcList.Add(npc);
        }
    }

    /// <summary>
    /// 将NPC从管理器中移除
    /// </summary>
    /// <param name="npc">NPC</param>
    public void UnRegistNPC(NPCBaseCharacter npc)
    {
        if (npcList.Contains(npc))
        {
            npcList.Remove(npc);
        }
    }
}
