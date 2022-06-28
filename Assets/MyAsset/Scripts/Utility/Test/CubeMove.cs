using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed = 0.2f;

    private Vector3 newDirection;
    private float horizontal;
    private float vertical;
    private Vector3 TargetDirection { get { return transform.position - Camera.main.transform.position; } }

    private void Start()
    {
        Debug.Log($"移动鼠标转换视角");
    }

    private void Update()
    {
        // 在水平面上转向摄像机方向
        newDirection = Vector3.RotateTowards(transform.forward, TargetDirection, rotateSpeed, 0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
        // 记录移动输入
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // 在前后方向移动
        transform.position += vertical * moveSpeed * Time.deltaTime * new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Camera.main.transform.position += vertical * moveSpeed * Time.deltaTime * new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        // 在左右方向移动
        transform.position += horizontal * moveSpeed * Time.deltaTime * new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        Camera.main.transform.position += horizontal * moveSpeed * Time.deltaTime * new Vector3(transform.right.x, 0f, transform.right.z).normalized;
    }
}
