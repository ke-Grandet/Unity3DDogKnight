using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerConfig
{
    public string Character;
    public string PrefabPath;
    public string HolyPrefab;
}


public class PlayerManager : Singleton<PlayerManager>
{
    private PlayerConfig config;
    private readonly Transform originPosition;
    private DogKnightCharacter playerCharacter;
    private HolyCharacter holyCharacter;

    public DogKnightCharacter PlayerCharacter { get { return playerCharacter; } }
    public HolyCharacter Holy { get { return holyCharacter; } }

    private PlayerManager()
    {
        // �ҵ���������ҳ�ʼ����λ��
        GameObject playerPosition = GameObject.FindWithTag(StringTag.Player);
        if (playerPosition != null)
        {
            Debug.Log("�ҵ���ҳ�ʼλ��");
            originPosition = playerPosition.transform;
        }
        else
        {
            Debug.Log("δ�ҵ���ҳ�ʼλ�ã�");
            return;
        }
        CreateDogKnight();
        CreateHoly();
    }

    public void Initial()
    {
        playerCharacter.transform.SetPositionAndRotation(originPosition.position, Quaternion.identity);
        playerCharacter.Initial();
    }

    private void CreateDogKnight()
    {
        // ��ȡ�������
        config = ResourceManager.Instance.FindResourceFromJson<PlayerConfig>(StringConfigPath.Player_Config);
        // �������
        GameObject prefab = ResourceManager.Instance.FindResource(config.PrefabPath);
        GameObject player = Object.Instantiate(prefab);
        player.name = player.name.Replace("(Clone)", "");
        player.transform.SetParent(originPosition, false);
        // ��Ϊû�ж�character����������ʾָ��DogKnightCharacter
        //System.Type characterType = System.Type.GetType(config.Character);
        playerCharacter = player.AddComponent<DogKnightCharacter>();
    }

    private void CreateHoly()
    {
        GameObject holyPrefab = ResourceManager.Instance.FindResource(config.HolyPrefab);
        GameObject holy = Object.Instantiate(holyPrefab);
        holy.name = holy.name.Replace("(Clone)", "");
        holy.transform.position = Vector3.zero;
        holyCharacter = holy.AddComponent<HolyCharacter>();
    }

    public void GameOver()
    {
        holyCharacter.GameOver();
    }
}
