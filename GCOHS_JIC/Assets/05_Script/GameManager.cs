using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public List<RectArea> 馬樹放置範圍清單;
    public RectArea 土坡放置範圍;

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
        foreach (var area in this.馬樹放置範圍清單)
        {
            if (area.左上 != null && area.右下 != null)
            {
                //顯示左上、右下兩點
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(area.左上.position, 10);
                Gizmos.DrawSphere(area.右下.position, 10);

                //劃出邊界
                Gizmos.color = Color.red;
                Gizmos.DrawRay(area.左上.position, Vector3.right * Mathf.Abs(area.左上.position.x - area.右下.position.x));
                Gizmos.DrawRay(area.右下.position, Vector3.left * Mathf.Abs(area.左上.position.x - area.右下.position.x));
                Gizmos.DrawRay(area.左上.position, Vector3.down * Mathf.Abs(area.左上.position.y - area.右下.position.y));
                Gizmos.DrawRay(area.右下.position, Vector3.up * Mathf.Abs(area.左上.position.y - area.右下.position.y));
            }
        }

        if (土坡放置範圍.左上 != null && 土坡放置範圍.右下 != null)
        {
            //顯示左上、右下兩點
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(土坡放置範圍.左上.position, 10);
            Gizmos.DrawSphere(土坡放置範圍.右下.position, 10);

            //劃出邊界
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(土坡放置範圍.左上.position, Vector3.right * Mathf.Abs(土坡放置範圍.左上.position.x - 土坡放置範圍.右下.position.x));
            Gizmos.DrawRay(土坡放置範圍.右下.position, Vector3.left * Mathf.Abs(土坡放置範圍.左上.position.x - 土坡放置範圍.右下.position.x));
            Gizmos.DrawRay(土坡放置範圍.左上.position, Vector3.down * Mathf.Abs(土坡放置範圍.左上.position.y - 土坡放置範圍.右下.position.y));
            Gizmos.DrawRay(土坡放置範圍.右下.position, Vector3.up * Mathf.Abs(土坡放置範圍.左上.position.y - 土坡放置範圍.右下.position.y));
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
