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
    // �������
    private Camera camera;
    // �������
    private Transform target;
    // ���ֵľ���
    private Vector3 distance;
    // ƽ���ƶ������ʱ��
    private readonly float smoothTime;
    // �����ٶȣ���ref��ֵ
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
        Debug.Log("�������Ŀ���ǣ�" + target.name);
    }

    public void OnLateUpdate()
    {
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target.position + distance, ref velocity, smoothTime);

    }
}
