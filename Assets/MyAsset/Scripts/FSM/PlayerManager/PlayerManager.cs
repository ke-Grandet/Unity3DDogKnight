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
        // 找到场景中玩家初始化的位置
        GameObject playerPosition = GameObject.FindWithTag(StringTag.Player);
        if (playerPosition != null)
        {
            Debug.Log("找到玩家初始位置");
            originPosition = playerPosition.transform;
        }
        else
        {
            Debug.Log("未找到玩家初始位置！");
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
        // 读取玩家配置
        config = ResourceManager.Instance.FindResourceFromJson<PlayerConfig>(StringConfigPath.Player_Config);
        // 创建玩家
        GameObject prefab = ResourceManager.Instance.FindResource(config.PrefabPath);
        GameObject player = Object.Instantiate(prefab);
        player.name = player.name.Replace("(Clone)", "");
        player.transform.SetParent(originPosition, false);
        // 因为没有对character抽象，所以显示指定DogKnightCharacter
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
