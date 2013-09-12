using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager script;

    public GameObject 物件區背景;
    public GameObject 現代郎世寧;
    public GameObject StageHintObject;
    private GameObject currentStageHintObject;
    public GameObject TitleShowObject;
    public DrawStage CurrentDrawStage;

    public List<RectArea> 馬樹放置範圍清單;
    public RectArea 土坡放置範圍;

    public Transform scaleLineTop;
    public Transform scaleLineBottom;

    public SetColorBoneAnimation.PictureType 設色潑墨顏色;

    [HideInInspector]
    public string 馬1_GUID;
    [HideInInspector]
    public string 馬2_GUID;
    [HideInInspector]
    public string 馬3_GUID;
    [HideInInspector]
    public string 樹1_GUID;
    [HideInInspector]
    public string 樹2_GUID;
    [HideInInspector]
    public string 土坡1_GUID;
    [HideInInspector]
    public string 土坡2_GUID;
    [HideInInspector]
    public string 土坡3_GUID;

    [HideInInspector]
    public string[] 馬1顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 馬2顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 馬3顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 樹1顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 樹2顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 土坡1顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 土坡2顏色_GUID = new string[3];
    [HideInInspector]
    public string[] 土坡3顏色_GUID = new string[3];

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        //遊戲開始，首先進入開頭動畫
        this.ChangeDrawStage(DrawStage.開頭動畫);
        this.現代郎世寧.SetActive(false);        //將左上"現代郎世寧"暫時關閉
    }

    /// <summary>
    /// 切換作畫階段
    /// </summary>
    /// <param name="nextStage">下一階段</param>
    public void ChangeDrawStage(DrawStage nextStage)
    {
        if (nextStage == DrawStage.等待中 || nextStage > DrawStage.截圖)
            return;

        if (this.currentStageHintObject != null)
            Destroy(this.currentStageHintObject);

        this.物件區背景.SetActive(false);

        if (nextStage == DrawStage.截圖)
        {
            this.CurrentDrawStage = DrawStage.截圖;
            State.script.光源的控制桿.SetActive(false);
            return;
        }

        GameObject obj = (GameObject)Instantiate(this.TitleShowObject);
        SmoothMoves.BoneAnimation boneAnimation = obj.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
        boneAnimation.Play(nextStage.ToString());

        //開頭動畫狀態進行觸碰事件偵測
        if (nextStage == DrawStage.開頭動畫)
            this.CurrentDrawStage = DrawStage.開頭動畫;
        else
            this.CurrentDrawStage = DrawStage.等待中;
    }

    /// <summary>
    /// 開啟當前階段的提示
    /// </summary>
    /// <param name="currentStage">目前進行階段</param>
    public void ShowStageHint(DrawStage currentStage)
    {
        this.currentStageHintObject = (GameObject)Instantiate(this.StageHintObject);
        SmoothMoves.BoneAnimation boneAnimation = this.currentStageHintObject.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
        boneAnimation.Play(currentStage.ToString());
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

    /// <summary>
    /// 作畫階段
    /// </summary>
    public enum DrawStage : int
    {
        等待中 = 0, 開頭動畫 = 1, 構圖 = 2, 明暗 = 3, 設色 = 4, 淡化 = 5, 光源 = 6, 截圖 = 7
    }
}
