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

    private GameObject Target;

    private RaycastHit hit;

    private MouseType mouseType;
    private PictureInfo.PictureType currentPictureType;

    //按下累積時間
    private float clickTime;
    //按下累積時間門檻
    public float clickTime_threshold;

    public GameObject 馬樹範圍提示物件;
    public GameObject 土坡範圍提示物件;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer) == true)
        {
            this.ProcessMouseState();
        }
        else
        {
            //當超出範圍時(例如滑鼠移動太快，圖片沒跟到的時候) 要把圖片ALPHA調回１
            this.mouseType = MouseType.放開;
            iTween.StopByName("PickObject");
            if (Target)
            {
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
                            break;
                        }
                    }
                }
                else if (this.currentPictureType == PictureInfo.PictureType.土坡)
                {
                    if (GameManager.script.土坡放置範圍.isContainArea(this.Target.transform.position))
                        isContain = true;
                }

                if (!isContain)
                    this.Target.GetComponent<PictureInfo>().BacktoOriginPosition();

            }

            this.Target = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1) * 100);
    }


    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        // 狀態:點擊 滑鼠按下時間>門檻時 改變狀態為拖曳
        if (this.mouseType == MouseType.點擊)
        {
            this.clickTime += Time.deltaTime;
            if (this.clickTime > this.clickTime_threshold)
            {
                this.mouseType = MouseType.拖曳中;

                iTween.ValueTo(this.gameObject, iTween.Hash(
                       "name", "PickObject",
                       "from", 1,
                       "to", 0.45F,
                       "onupdate", "changePictureAlpha",
                       "loopType", "pingPong",
                       "time", 1
                       ));

                this.currentPictureType = this.Target.GetComponent<PictureInfo>().Type;

                if (this.Target)
                    print(this.Target.transform.name + " 已被選取");
            }
        }
        //狀態：拖曳　改變XY座標　當滑鼠放開時　狀態：放開
        if (this.mouseType == MouseType.拖曳中)
        {
            if (this.Target)
            {
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
                this.Target.GetComponent<PictureInfo>().ChangeScale();
            }


            if (!Input.GetKey(KeyCode.Mouse0))
            {
                this.mouseType = MouseType.放開;
            }
        }

        //狀態：放開　當滑鼠點擊時　狀態：點擊
        if (mouseType == MouseType.放開)
        {
            this.Target = this.hit.transform.gameObject;

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
                        break;
                    }
                }
            }
            else if (this.currentPictureType == PictureInfo.PictureType.土坡)
            {
                if (GameManager.script.土坡放置範圍.isContainArea(this.Target.transform.position))
                    isContain = true;
            }

            if (!isContain)
                this.Target.GetComponent<PictureInfo>().BacktoOriginPosition();

            this.clickTime = 0;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                this.mouseType = MouseType.點擊;
            }
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

    //定義滑鼠狀態
    public enum MouseType
    {
        點擊 = 0, 拖曳中 = 1, 放開 = 2
    }
}
