using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLines : MonoBehaviour
{
    public Material LineMaterial;
    public float LineSize;

    private Vector3 currentPoint;
    private Vector3 lastPoint = new Vector3(0, 0, -100.0f);

    private int currentLineIndex = 0;

    void Update()
    {
        //���եΡA�M�ũҦ��e�u
        if (Input.GetKeyDown(KeyCode.C))
            foreach (var line in this.transform.GetComponentsInChildren<LineRenderer>())
            {
                Destroy(line.gameObject);
                this.currentLineIndex = 0;
            }


        if (Input.GetMouseButton(0))
        {
            this.currentPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            if (this.lastPoint.z != -100.0f)
                this.CreateLine(this.lastPoint, this.currentPoint, this.LineSize);

            this.lastPoint = this.currentPoint;
        }
        else
            this.lastPoint.z = -100.0f;
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
}







