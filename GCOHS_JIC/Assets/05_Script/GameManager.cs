using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public RectArea ReleaseArea;

    public Transform scaleLineTop;
    public Transform scaleLineBottom;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    void OnDrawGizmos()
    {
        if (this.ReleaseArea.左上 != null && this.ReleaseArea.右下 != null)
        {
            //顯示左上、右下兩點
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(this.ReleaseArea.左上.position, 10);
            Gizmos.DrawSphere(this.ReleaseArea.右下.position, 10);

            //劃出邊界
            Gizmos.color = Color.red; ;
            Gizmos.DrawRay(this.ReleaseArea.左上.position, Vector3.right * Mathf.Abs(this.ReleaseArea.左上.position.x - this.ReleaseArea.右下.position.x));
            Gizmos.DrawRay(this.ReleaseArea.右下.position, Vector3.left * Mathf.Abs(this.ReleaseArea.左上.position.x - this.ReleaseArea.右下.position.x));
            Gizmos.DrawRay(this.ReleaseArea.左上.position, Vector3.down * Mathf.Abs(this.ReleaseArea.左上.position.y - this.ReleaseArea.右下.position.y));
            Gizmos.DrawRay(this.ReleaseArea.右下.position, Vector3.up * Mathf.Abs(this.ReleaseArea.左上.position.y - this.ReleaseArea.右下.position.y));
        }

        if (this.scaleLineTop != null && this.scaleLineBottom != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(this.scaleLineTop.position, this.scaleLineBottom.position);
        }
    }

    [System.Serializable]
    public class RectArea
    {
        public Transform 左上;
        public Transform 右下;

        public bool isContainArea(Vector3 pos)
        {
            float TempX = Mathf.Clamp(pos.x, this.左上.position.x, this.右下.position.x);
            float TempY = Mathf.Clamp(pos.y, this.右下.position.y, this.左上.position.y);

            if (TempX == this.左上.position.x | TempX == this.右下.position.x)
                return false;
            if (TempY == this.左上.position.y | TempY == this.右下.position.y)
                return false;

            return true;
        }
    }

}
