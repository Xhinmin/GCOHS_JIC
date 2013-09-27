using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLines : MonoBehaviour
{
    public static DrawLines script;

    public float FinishScale;
    public Vector2 FinishOffset;

    public GameObject 簽名提示範圍;

    public Material LineMaterial;
    public float LineSize;

    public GameManager.RectArea 簽名範圍;

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
            //測試用，清空所有畫線
            if (Input.GetKeyDown(KeyCode.C))
                this.ClearAllLine();

            if (Input.GetMouseButton(0))
            {
                this.currentPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

                if (this.lastPoint.z != -100.0f)
                    if (this.簽名範圍.isContainArea(this.currentPoint))
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
        GameManager.script.CurrentDrawStage = GameManager.DrawStage.等待中;
        this.簽名提示範圍.SetActive(false);
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
        lineRender.SetWidth(lineSize, lineSize);    //線條粗細
        lineRender.SetVertexCount(2);               //設定兩個頂點
        lineRender.SetPosition(0, start);           //線條起始點
        lineRender.SetPosition(1, end);             //線條終點

        this.currentLineIndex++;    //編號
    }

    void OnDrawGizmos()
    {
        if (this.簽名範圍.左上 != null && this.簽名範圍.右下 != null)
        {
            //顯示左上、右下兩點
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(this.簽名範圍.左上.position, 10);
            Gizmos.DrawSphere(this.簽名範圍.右下.position, 10);

            //劃出邊界
            Gizmos.color = Color.green;
            Gizmos.DrawRay(this.簽名範圍.左上.position, Vector3.right * Mathf.Abs(this.簽名範圍.左上.position.x - this.簽名範圍.右下.position.x));
            Gizmos.DrawRay(this.簽名範圍.右下.position, Vector3.left * Mathf.Abs(this.簽名範圍.左上.position.x - this.簽名範圍.右下.position.x));
            Gizmos.DrawRay(this.簽名範圍.左上.position, Vector3.down * Mathf.Abs(this.簽名範圍.左上.position.y - this.簽名範圍.右下.position.y));
            Gizmos.DrawRay(this.簽名範圍.右下.position, Vector3.up * Mathf.Abs(this.簽名範圍.左上.position.y - this.簽名範圍.右下.position.y));
        }
    }
}







