using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLines : MonoBehaviour
{
    public static DrawLines script;

    public float FinishScale;
    public Vector2 FinishOffset;

    public GameObject ñ�W���ܽd��;

    public Material LineMaterial;
    public float LineSize;

    public GameManager.RectArea ñ�W�d��;

    private Vector3 currentPoint;
    private Vector3 lastPoint = new Vector3(0, 0, -100.0f);

    private int currentLineIndex = 0;
    private bool canDraw = true;

    void Awake()
    {
        script = this;
    }

    void Update()
    {
        if (this.canDraw)
        {
            //���եΡA�M�ũҦ��e�u
            if (Input.GetKeyDown(KeyCode.C))
                this.ClearAllLine();

            if (Input.GetMouseButton(0))
            {
                this.currentPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

                if (this.lastPoint.z != -100.0f)
                    if (this.ñ�W�d��.isContainArea(this.currentPoint))
                    {
                        this.CreateLine(this.lastPoint, this.currentPoint, this.LineSize);
                    }

                this.lastPoint = this.currentPoint;
            }
            else
                this.lastPoint.z = -100.0f;
        }
    }

    public void FinishDrawLine()
    {
        this.canDraw = false;
        GameManager.script.CurrentDrawStage = GameManager.DrawStage.���ݤ�;
        this.ñ�W���ܽd��.SetActive(false);
        if (GameManager.script.CanonLogo != null)
            GameManager.script.CanonLogo.SetActive(true);
        this.transform.parent.localScale = Vector3.one * this.FinishScale;
        this.transform.parent.position += new Vector3(this.FinishOffset.x, this.FinishOffset.y, this.transform.parent.position.z);
        ScreenShot.script.RunScreenCapture();
    }

    public void ClearAllLine()
    {
        foreach (var line in this.transform.GetComponentsInChildren<LineRenderer>())
        {
            Destroy(line.gameObject);
            this.currentLineIndex = 0;
        }
    }

    void CreateLine(Vector3 start, Vector3 end, float lineSize)
    {
        GameObject newLine = new GameObject("Line" + currentLineIndex);
        newLine.transform.parent = transform;

        LineRenderer lineRender = (LineRenderer)newLine.AddComponent<LineRenderer>();
        lineRender.material = this.LineMaterial;
        lineRender.useWorldSpace = false;
        lineRender.SetWidth(lineSize, lineSize);    //�u���ʲ�
        lineRender.SetVertexCount(2);               //�]�w��ӳ��I
        lineRender.SetPosition(0, start);           //�u���_�l�I
        lineRender.SetPosition(1, end);             //�u�����I

        this.currentLineIndex++;    //�s��
    }

    void OnDrawGizmos()
    {
        if (this.ñ�W�d��.���W != null && this.ñ�W�d��.�k�U != null)
        {
            //��ܥ��W�B�k�U���I
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(this.ñ�W�d��.���W.position, 10);
            Gizmos.DrawSphere(this.ñ�W�d��.�k�U.position, 10);

            //���X���
            Gizmos.color = Color.green;
            Gizmos.DrawRay(this.ñ�W�d��.���W.position, Vector3.right * Mathf.Abs(this.ñ�W�d��.���W.position.x - this.ñ�W�d��.�k�U.position.x));
            Gizmos.DrawRay(this.ñ�W�d��.�k�U.position, Vector3.left * Mathf.Abs(this.ñ�W�d��.���W.position.x - this.ñ�W�d��.�k�U.position.x));
            Gizmos.DrawRay(this.ñ�W�d��.���W.position, Vector3.down * Mathf.Abs(this.ñ�W�d��.���W.position.y - this.ñ�W�d��.�k�U.position.y));
            Gizmos.DrawRay(this.ñ�W�d��.�k�U.position, Vector3.up * Mathf.Abs(this.ñ�W�d��.���W.position.y - this.ñ�W�d��.�k�U.position.y));
        }
    }
}







