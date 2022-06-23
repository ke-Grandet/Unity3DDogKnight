using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NPCConfig
{
    public NPCSpawn[] NPCArr;
}
[System.Serializable]
public class NPCSpawn
{
    public string PrefabPath;
    public float PositionX;
    public float PositionY;
    public float PositionZ;
    public string AsString()
    {
        return $"prefab={PrefabPath}, x={PositionX}, y={PositionY}, z={PositionZ}";
    }
}

public class NPCTrigger
{
    private readonly NPCConfig config;

    public NPCTrigger()
    {
        config = ResourceManager.Instance.FindResourceFromJson<NPCConfig>(StringConfigPath.NPC_Config);
    }

    public void EnemyCome(Transform defaultTarget)
    {
        for (int i = 0; i < config.NPCArr.Length; i++)
        {
            //Debug.Log(config.NPCArr[i].AsString());
            Vector3 position = new(config.NPCArr[i].PositionX, config.NPCArr[i].PositionY, config.NPCArr[i].PositionZ);
            NPCManager.Instance.GetNPC<GruntCharacter>(position, config.NPCArr[i].PrefabPath, defaultTarget);
        }
    }

}
