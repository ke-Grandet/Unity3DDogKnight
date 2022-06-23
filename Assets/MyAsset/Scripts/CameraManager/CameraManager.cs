using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CameraConfig
{
    public float DistanceX = 0f;
    public float DistanceY = 4.5f;
    public float DistanceZ = -3.5f;
    public float SmoothTime = 0.2f;
}

public class CameraManager : Singleton<CameraManager>
{
    private readonly CameraConfig config = new();
    // 相机对象
    private Camera camera;
    // 跟随对象
    private Transform target;
    // 保持的距离
    private Vector3 distance;
    // 平滑移动所需的时间
    private readonly float smoothTime;
    // 跟随速度，由ref赋值
    private Vector3 velocity = Vector3.one;

    public Camera Camera { get { return camera; } set { camera = value; } }
    public Transform Target { get { return target; } set { target = value; } }

    private CameraManager()
    {
        config = ResourceManager.Instance.FindResourceFromJson<CameraConfig>(StringConfigPath.Camera_Config);
        distance = new(config.DistanceX, config.DistanceY, config.DistanceZ);
        smoothTime = config.SmoothTime;
        camera = Camera.main;
        target = PlayerManager.Instance.PlayerCharacter.transform;
        Debug.Log("相机跟随目标是：" + target.name);
    }

    public void OnLateUpdate()
    {
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target.position + distance, ref velocity, smoothTime);

    }
}
