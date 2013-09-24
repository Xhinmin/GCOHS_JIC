using UnityEngine;
using System.Collections;

//【第五階段】給光源用的滑鼠拖曳
public class ClickObjectStep4 : MonoBehaviour
{
    public static ClickObjectStep4 script;
    public Camera ViewCamera;
    public LayerMask TargetLayer;
    private GameObject Target;
    private RaycastHit hit;
    public MouseType currentMouseType;

    public int 物件中心點Center = 0;
    public int 物件位置Min = -600;
    public int 物件位置Max = 600;
    //與中心點的距離
    private int dis;

    public GameObject 對應土坡圖;


    // Use this for initialization
    void Start()
    {
        script = this;
        dis = Mathf.Abs(物件中心點Center - 物件位置Min);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouseState();
    }

    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        switch (this.currentMouseType)
        {
            case MouseType.初始播放引導動畫:
                //播放引導動畫
                PlayHandBoneAnimation.script.animationType = PlayHandBoneAnimation.AnimationType.太陽引導;
                // PlayHintBoneAnimation.script.animationType = PlayHintBoneAnimation.AnimationType.畫布上方太陽;
                this.currentMouseType = MouseType.無狀態;
                break;

            case MouseType.無狀態:
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.點擊;
                break;

            case MouseType.點擊:

                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                    this.currentMouseType = MouseType.拖曳中;
                else
                    this.currentMouseType = MouseType.無狀態;
                break;

            case MouseType.拖曳中:

                //拖曳後解鎖
                ClickObject.script.isLock = false;


                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                {
                    //關閉手指動畫
                    PlayHandBoneAnimation.script.animationType = PlayHandBoneAnimation.AnimationType.空動畫;

                    //物件拖曳範圍 Min~Max
                    this.hit.transform.position = new Vector3(
                       Mathf.Max(Mathf.Min(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).x, 物件位置Max), 物件位置Min),
                       this.hit.transform.position.y,
                       this.hit.transform.position.z);

                    //土坡物件的淡化漸變 ALPHA 0.5 -> 1.0
                    //最左邊為0.5 最右為1.0 雙倍的與中心點的距離
                    對應土坡圖.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 0.6F + 0.4F * (float)((this.hit.transform.position.x - 物件位置Min) / (dis * 2)));
                    對應土坡圖.GetComponent<SmoothMoves.Sprite>().UpdateArrays();
                    //使狀態更正為已使用 通知下一步按鈕出現
                    對應土坡圖.GetComponent<PictureInfo>().isUsed = true;

                }
                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.無狀態;

                break;

            case MouseType.放開:

                this.currentMouseType = MouseType.無狀態;
                break;
        }


    }

    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 拖曳中 = 2, 放開 = 3, 初始播放引導動畫 = 4
    }
}