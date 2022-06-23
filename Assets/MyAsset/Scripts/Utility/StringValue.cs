using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// UI面板相关的预制体路径
/// </summary>
public class StringUIPanelPath
{
    public static readonly string healthPanel = "Prefab/UI/HealthPanel";
    public static readonly string loginPanel = "Prefab/UI/LoginPanel";
    public static readonly string registPanel = "Prefab/UI/RegistPanel";
    public static readonly string joystickPanel = "Prefab/UI/JoystickPanel";
    public static readonly string skillPanel = "Prefab/UI/SkillPanel";
    public static readonly string npcHealthBar = "Prefab/UI/NPCHealthBar";
    public static readonly string litmapPanel = "Prefab/UI/litmapPanel";
    // 主菜单、暂停菜单、游戏结束菜单
    public static readonly string MainMenuPanel = "Prefab/UI/Menu/MainMenuPanel";
    public static readonly string PauseMenuPanel = "Prefab/UI/Menu/PauseMenuPanel";
    public static readonly string FailMenuPanel = "Prefab/UI/Menu/FailMenuPanel";
}


/// <summary>
/// 数据文件的路径
/// </summary>
public static class StringDataPath
{
    public const string DogKnight_Simple_Data = "Data/DogKnightSimpleData";
    public const string Grunt_Simple_Data = "Data/GruntSimpleData";
    public const string Holy_Simple_Data = "Data/HolySimpleData";
}


/// <summary>
/// 各配置文件的路径
/// </summary>
public static class StringConfigPath
{
    //public static readonly string audioClipConfig = System.IO.Path.Combine(Application.streamingAssetsPath, "AudioClipConfig.json");
    public const string UI_Panel_Config = "Config/UIPanelConfig";
    public const string Player_Config = "Config/PlayerConfig";
    public const string NPC_Config = "Config/NPCConfig";
    public const string Camera_Config = "Config/CameraConfig";
    public const string Audio_Config = "Config/AudioConfig";
}


public static class StringAudioName
{
    public const string Normal_Music = "BGM_04";
    public const string Fight_Music = "BGM_02";
    public const string Sword_Attack = "Swing1-Free-1";
    public const string Axe_Attack = "AWP_Swing_Axe_01";
}


/// <summary>
/// 自定义的Tag
/// </summary>
public static class StringTag
{
    public const string Player = "Player";
    public const string Main_Canvas = "MainCanvas";
    public const string World_Canvas = "WorldCanvas";
}


/// <summary>
/// 自定义的图层
/// </summary>
public static class StringLayerName
{
    public const string Enemy = "Enemy";
    public static int Layer_Enemy = LayerMask.NameToLayer(Enemy);
    public static int LayerMask_Enemy = 1 << Layer_Enemy;

    public const string Player = "Player";
    public static int Layer_Player = LayerMask.NameToLayer(Player);
    public static int LayerMask_Player = 1 << Layer_Player;

    public static string Holy = "Holy";
    public static int Layer_Holy = LayerMask.NameToLayer(Holy);
    public static int LayerMask_Holy = 1 << Layer_Holy;
}


/// <summary>
/// 输入轴
/// </summary>
public static class StringAxis
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}