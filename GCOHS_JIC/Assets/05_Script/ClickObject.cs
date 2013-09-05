using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第二步驟：明暗
/// </summary>
/// 
public class ClickObject : MonoBehaviour
{
    public static ClickObject script;
    public Camera ViewCamera;
    public LayerMask TargetLayer;
    private GameObject Target;
    private RaycastHit[] hits;
    public MouseType currentMouseType;
    RaycastHit innerHit;

    //是否可以選擇下一個　物件　的鎖定狀態
    public bool isLock;

    // Use this for initialization
    void Start()
    {
        script = this;
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
            case MouseType.無狀態:
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.點擊;
                break;

            case MouseType.點擊:
                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.放開;
                break;

            case MouseType.放開:

                hits = Physics.RaycastAll(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), 200, this.TargetLayer);
                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.gameObject.GetComponent<PictureInfo>().isBlink)
                        {
                            innerHit = hit;
                            //如果切換圖片後　再解除Lock 才能賦予新的Target [0904更新 不需要Lock]
                            if (innerHit.transform.gameObject.GetComponent<PictureInfo>().isBlink)
                            {
                                //【明暗】、【淡化】
                                if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗 ||
                                    GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化)
                                {
                                    if (!this.isLock)
                                    {
                                        isLock = true;
                                        ClearControlArea();
                                        this.Target = innerHit.transform.gameObject;
                                    }
                                }
                                //【設色】
                                else
                                {
                                    if (Target)
                                    {
                                        this.Target.GetComponent<PictureInfo>().isBlink = true;
                                        ClearControlArea(); //清空操作區
                                        if (this.Target.GetComponent<PictureInfo>().isBlink) this.Target = innerHit.transform.gameObject; // 給予新的Target
                                    }
                                    else
                                        this.Target = innerHit.transform.gameObject;
                                }



                                //停止閃爍 並將顏色還原
                                this.Target.GetComponent<PictureInfo>().isBlink = false;
                                this.Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1));

                                if (this.Target.GetComponent<Step2>()) this.Target.GetComponent<Step2>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗 ? true : false;
                                if (this.Target.GetComponent<Step3>()) this.Target.GetComponent<Step3>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.設色 ? true : false;
                                if (this.Target.GetComponent<Step4>()) this.Target.GetComponent<Step4>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化 ? true : false;
                                //this.Target.GetComponent<Step5>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage. ? true : false;

                                break;
                            }
                        }
                    }
                }

                this.currentMouseType = MouseType.無狀態;
                break;
        }


    }


    /// <summary>
    /// 改變Target的圖片 , 更換圖片後解鎖 【設色】
    /// </summary>
    public void SetPictureStep3(GameObject ChangeObject)
    {
        this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(ChangeObject.GetComponent<SmoothMoves.Sprite>().textureGUID);
        this.Target.GetComponent<PictureInfo>().isUsed = true;
    }

    /// <summary>
    /// 改變操作區 構圖的圖片變明暗圖 , 並將Target一同改變成明暗圖 【明暗】
    /// </summary>
    public void SetPictureStep2(GameObject ChangeObject)
    {
        isLock = false;
        if (Target.gameObject.name == "馬1")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬1_GUID);
            GameObject.Find("馬1-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬1_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "馬2")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬2_GUID);
            GameObject.Find("馬2-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬2_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "馬3")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬3_GUID);
            GameObject.Find("馬3-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬3_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "樹1")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹1_GUID);
            GameObject.Find("樹1-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹1_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "樹2")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹2_GUID);
            GameObject.Find("樹2-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹2_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }
    }


    /// <summary>
    /// 改變操作區 構圖的圖片變明暗圖 , 並將Target一同改變成明暗圖 【淡化】
    /// </summary>
    public void SetPictureStep4()
    {
        isLock = false;
        if (Target.gameObject.name == "土坡1(物件)")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡1_GUID);
            GameObject.Find("土坡1-淡化").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡1_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "土坡2(物件)")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡2_GUID);
            GameObject.Find("土坡2-淡化").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡2_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }

        if (Target.gameObject.name == "土坡3(物件)")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡3_GUID);
            GameObject.Find("土坡3-淡化").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.土坡3_GUID);
            this.Target.GetComponent<PictureInfo>().isUsed = true;
        }
    }



    /// <summary>
    /// 清空操作區 0904加入
    /// </summary>
    public void ClearControlArea()
    {
        if (Target)
        {
            if (this.Target.GetComponent<Step2>()) this.Target.GetComponent<Step2>().enabled = false;
            if (this.Target.GetComponent<Step3>()) this.Target.GetComponent<Step3>().enabled = false;
            if (this.Target.GetComponent<Step4>()) this.Target.GetComponent<Step4>().enabled = false;

        }
    }



    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 放開 = 3
    }
}
