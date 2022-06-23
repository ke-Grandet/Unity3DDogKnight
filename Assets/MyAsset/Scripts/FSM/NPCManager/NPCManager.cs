using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    private readonly List<NPCBaseCharacter> npcList;  // ���������е�NPC
    private readonly Dictionary<System.Type, List<NPCBaseCharacter>> npcPoolDic;  // NPC����أ�key��NPC��ɫ����

    public List<NPCBaseCharacter> NPCList { get { return npcList; } }

    private NPCManager()
    {
        npcList = new();
        npcPoolDic = new();
    }

    /// <summary>
    /// �Ӷ������ȡ��NPC
    /// </summary>
    /// <typeparam name="CharacterType">NPC�Ľ�ɫ����</typeparam>
    /// <param name="parent">NPC�ĸ�����</param>
    /// <param name="prefabPath">NPC��Ԥ����·��</param>
    /// <param name="useNewPrefab">�Ƿ�ʹ�ø���Ԥ�����½�NPC</param>
    /// <returns>NPC����</returns>
    public CharacterType GetNPC<CharacterType>(Vector3 position, string prefabPath, Transform defaultTarget=null, bool useNewPrefab=false) where CharacterType : NPCBaseCharacter
    {
        //Debug.Log($"�Ӷ������ȡ��NPC{prefabPath}");
        // �ֵ����Ƿ���ڸö�����б�
        if (!npcPoolDic.TryGetValue(typeof(CharacterType), out List<NPCBaseCharacter> npcPoolList))
        {
            npcPoolList = new();
            npcPoolDic[typeof(CharacterType)] = npcPoolList;
        }
        CharacterType npc;
        // ���ݲ����Ͷ���������ж��Ƿ��½�Ԥ�������
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
        //Debug.Log("����λ���ǣ� " + position);
        npc.transform.position = position;
        npc.DefaultTarget = defaultTarget;
        return npc;
    }

    /// <summary>
    /// ��NPC���ض����
    /// </summary>
    /// <typeparam name="CharacterType">NPC�ľ����ɫ����</typeparam>
    /// <param name="npc">Ҫ���ͻص�NPC����</param>
    public void ReturnNPC(NPCBaseCharacter npc)
    {
        //Debug.Log($"NPC{npc.name}�����ض����");
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
    /// ���ճ���������NPC
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
    /// ��NPC���뵽��������
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
    /// ��NPC�ӹ��������Ƴ�
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
