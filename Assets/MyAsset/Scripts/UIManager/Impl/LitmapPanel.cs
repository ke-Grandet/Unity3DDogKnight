using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LitmapPanel : UIBasePanel
{
    private RectTransform litmapRect;
    // GL
    private readonly float positionOffsetX = -16f;
    private readonly float positionOffsetZ = -16f;
    private readonly float mapRealWidth = 64f;
    private readonly float mapRealHeight = 64f;

    private float percentZ;
    private float percentX;
    private Vector3 litmapPosition;

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        litmapRect = GetWidget<RectTransform>("Litmap_N");
        CreateLineMaterial();
    }

    private void OnRenderObject()
    {
        if (PlayerManager.Instance.PlayerCharacter != null)
        {
            lineMaterial.SetPass(0);
            GL.MultMatrix(litmapRect.transform.localToWorldMatrix);  // �����Ƶ�ת����Ŀ������ϵ��
            //GL.LoadOrtho();  // �л�����ģʽ
            //GL.PushMatrix();
            // �������λ��
            GL.Begin(GL.QUADS);
            litmapPosition = CalculateLitmapPosition(PlayerManager.Instance.PlayerCharacter.transform);
            GL.Color(Color.green);
            GL.Vertex3(litmapPosition.x + 2.5f, litmapPosition.y + 2.5f, 0);
            GL.Vertex3(litmapPosition.x + 2.5f, litmapPosition.y - 2.5f, 0);
            GL.Vertex3(litmapPosition.x - 2.5f, litmapPosition.y - 2.5f, 0);
            GL.Vertex3(litmapPosition.x - 2.5f, litmapPosition.y + 2.5f, 0);
            GL.End();
            // ���ƻ���λ��
            GL.Begin(GL.QUADS);
            litmapPosition = CalculateLitmapPosition(PlayerManager.Instance.Holy.transform);
            GL.Color(Color.yellow);
            GL.Vertex3(litmapPosition.x + 2.5f, litmapPosition.y + 2.5f, 0);
            GL.Vertex3(litmapPosition.x + 2.5f, litmapPosition.y - 2.5f, 0);
            GL.Vertex3(litmapPosition.x - 2.5f, litmapPosition.y + 2.5f, 0);
            GL.Vertex3(litmapPosition.x - 2.5f, litmapPosition.y - 2.5f, 0);
            GL.End();
            // ���Ƶ���λ��
            foreach(var npc in NPCManager.Instance.NPCList)
            {
                GL.Begin(GL.TRIANGLE_STRIP);
                litmapPosition = CalculateLitmapPosition(npc.transform);
                GL.Color(Color.red);
                GL.Vertex3(litmapPosition.x, litmapPosition.y + 2.5f, 0);
                GL.Vertex3(litmapPosition.x + 2.5f, litmapPosition.y - 2.5f, 0);
                GL.Vertex3(litmapPosition.x - 2.5f, litmapPosition.y - 2.5f, 0);
                GL.End();
            }
            //GL.PopMatrix();
        }
    }

    private Vector3 CalculateLitmapPosition(Transform target)
    {
        // Ŀ���ڵ�ͼ�ı���λ��
        percentX = (target.position.x + positionOffsetX) / mapRealWidth;
        percentZ = (target.position.z + positionOffsetZ) / mapRealHeight;
        // �������������С��ͼ��ߵõ�С��ͼλ��
        return new(percentX * litmapRect.sizeDelta.x, percentZ * litmapRect.sizeDelta.y, 0);
    }
}
