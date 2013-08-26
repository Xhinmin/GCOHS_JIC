using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第一步驟：選取圖片
/// </summary>
/// 
public class PickObject : MonoBehaviour
{
    public Camera ViewCamera;

    public LayerMask TargetLayer;

    public GameObject 馬樹範圍提示物件;
    public GameObject 土坡範圍提示物件;

    private GameObject Target;

    private MouseType currentMouseType;
    private PictureInfo.PictureType currentPictureType;

    //按下累積時間
    private float clickTime;
    //按下累積時間門檻
    public float clickTime_threshold;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        this.ProcessMouseState();
        if (this.currentMouseType == MouseType.無狀態)
            this.Target = null;
    }

    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        switch (this.currentMouseType)
        {
            case MouseType.無狀態:
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.點擊;
                break;

            case MouseType.點擊:
                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer))
                {
                    this.clickTime += Time.deltaTime;
                    if (this.clickTime > this.clickTime_threshold)
                    {
                        this.Target = this.hit.transform.gameObject;
                        this.clickTime = 0;

                        if (!this.Target.GetComponent<PictureInfo>().CanMove)
                        {
                            this.currentMouseType = MouseType.無狀態;
                            return;
                        }

                        iTween.ValueTo(this.gameObject, iTween.Hash(
                               "name", "PickObject",
                               "from", 1,
                               "to", 0.5F,
                               "onupdate", "changePictureAlpha",
                               "loopType", "pingPong",
                               "time", 1
                               ));

                        this.currentPictureType = this.Target.GetComponent<PictureInfo>().Type;
                        this.currentMouseType = MouseType.拖曳中;
                    }
                }

                break;

            case MouseType.拖曳中:
                if (this.currentPictureType == PictureInfo.PictureType.馬樹)
                {
                    //將中心點修正
                    float offsetY = this.Target.GetComponent<SmoothMoves.Sprite>().size.y * this.Target.transform.localScale.y * 0.5f;

                    this.Target.transform.position = new Vector3(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).x,
                        this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).y - offsetY, this.Target.transform.position.z);
                }
                else if (this.currentPictureType == PictureInfo.PictureType.土坡)
                {
                    this.Target.transform.position = new Vector3(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).x,
                        this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).y, this.Target.transform.position.z);
                }

                this.Target.GetComponent<PictureInfo>().ChangeScaleDepth();

                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.放開;

                break;

            case MouseType.放開:
                iTween.StopByName("PickObject");

                this.Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1));

                if (this.currentPictureType == PictureInfo.PictureType.馬樹)
                    this.馬樹範圍提示物件.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0));
                else if (this.currentPictureType == PictureInfo.PictureType.土坡)
                    this.土坡範圍提示物件.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0));

                bool isContain = false;
                if (this.currentPictureType == PictureInfo.PictureType.馬樹)
                {
                    foreach (var area in GameManager.script.馬樹放置範圍清單)
                    {
                        if (area.isContainArea(this.Target.transform.position))
                        {
                            isContain = true;
                            this.Target.GetComponent<PictureInfo>().isUsed = true;
                            break;
                        }
                    }
                }
                else if (this.currentPictureType == PictureInfo.PictureType.土坡)
                {
                    if (GameManager.script.土坡放置範圍.isContainArea(this.Target.transform.position))
                    {
                        isContain = true;

                        PictureInfo script = this.Target.GetComponent<PictureInfo>();
                        script.CanMove = false;
                        script.isUsed = true;

                        this.Target.transform.localPosition = new Vector3(0, 0, script.MaxDepth);
                    }
                }

                if (!isContain & this.currentPictureType != PictureInfo.PictureType.未定義)
                    this.Target.GetComponent<PictureInfo>().BacktoOriginPosition();

                this.currentPictureType = PictureInfo.PictureType.未定義;
                this.currentMouseType = MouseType.無狀態;

                break;
        }
    }

    void changePictureAlpha(float newValue)
    {
        if (this.Target)
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, newValue));

            if (this.currentPictureType == PictureInfo.PictureType.馬樹)
                this.馬樹範圍提示物件.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0.9f - newValue));

            else if (this.currentPictureType == PictureInfo.PictureType.土坡)
                this.土坡範圍提示物件.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0.9f - newValue));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1) * 100);
    }

    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 拖曳中 = 2, 放開 = 3
    }
}
