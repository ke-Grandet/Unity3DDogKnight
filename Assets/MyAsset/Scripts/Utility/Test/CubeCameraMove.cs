using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeCameraMove : MonoBehaviour
{
    public Transform target;

    private Vector3 mousePositionStart;
    private Vector3 mousePositionEnd;
    private float deltaX;
    private float deltaY;
    private float rad;

    public float DistanceToTarget { get { return Vector3.Distance(transform.position, target.position); } }
    public Vector3 YVector { get { return Vector3.up; } }
    public Vector3 XVector { get { return Vector3.Cross(target.position - transform.position, Vector3.up); } }

    void Update()
    {

    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePositionEnd = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && mousePositionEnd != Vector3.negativeInfinity)
        {
            mousePositionStart = mousePositionEnd;
            mousePositionEnd = Input.mousePosition;
            // 变换上下角度
            deltaY = mousePositionEnd.y - mousePositionStart.y;
            deltaY = deltaY / Screen.width * DistanceToTarget;
            rad = Mathf.Asin(deltaY / DistanceToTarget);
            transform.RotateAround(target.position, XVector, rad * Mathf.Rad2Deg);
            // 变换左右角度
            deltaX = mousePositionEnd.x - mousePositionStart.x;
            deltaX = deltaX / Screen.width * DistanceToTarget;
            rad = Mathf.Asin(deltaX / DistanceToTarget);
            transform.RotateAround(target.position, YVector, rad * Mathf.Rad2Deg);
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePositionEnd = Vector3.negativeInfinity;
        }
    }
}
