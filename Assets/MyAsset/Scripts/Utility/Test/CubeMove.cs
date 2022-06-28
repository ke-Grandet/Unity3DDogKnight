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
        Debug.Log($"�ƶ����ת���ӽ�");
    }

    private void Update()
    {
        // ��ˮƽ����ת�����������
        newDirection = Vector3.RotateTowards(transform.forward, TargetDirection, rotateSpeed, 0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
        // ��¼�ƶ�����
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // ��ǰ�����ƶ�
        transform.position += vertical * moveSpeed * Time.deltaTime * new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Camera.main.transform.position += vertical * moveSpeed * Time.deltaTime * new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        // �����ҷ����ƶ�
        transform.position += horizontal * moveSpeed * Time.deltaTime * new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        Camera.main.transform.position += horizontal * moveSpeed * Time.deltaTime * new Vector3(transform.right.x, 0f, transform.right.z).normalized;
    }
}
