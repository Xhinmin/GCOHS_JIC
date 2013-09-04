﻿using UnityEngine;
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
    private GameObject newTarget;
    private RaycastHit hit;
    private MouseType currentMouseType;

    //是否可以選擇下一個　物件　的鎖定狀態
    private bool isLock;

    private bool isNeedInit;
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
                {
                    this.currentMouseType = MouseType.點擊;
                }
                break;
            case MouseType.點擊:

                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer))
                {
                    //如果切換圖片後　再解除Lock 才能賦予新的Target [0904更新 不需要Lock]
                    if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗 || GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化)
                    {
                        if (!this.isLock)
                        {
                            ClearControlArea();
                            this.Target = this.hit.transform.gameObject;
                        }
                    }
                    else
                    {
                        if (Target)
                        {
                            this.newTarget = this.hit.transform.gameObject;
                            if (newTarget != Target)
                            {
                                isNeedInit = true;
                            }

                            //只要Init依次
                            if (isNeedInit)
                            {
                                this.Target.GetComponent<PictureInfo>().isBlink = true;
                                ClearControlArea();
                                this.Target = this.hit.transform.gameObject;
                                isNeedInit = false;
                            }

                        }
                        else
                            this.Target = this.hit.transform.gameObject;
                    }


                    {
                        //停止閃爍 並將顏色還原
                        this.Target.GetComponent<PictureInfo>().isBlink = false;
                        this.Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1));
                        //將指定功能打開 ... 好久沒這樣寫
                        this.Target.GetComponent<Step2>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗 ? true : false;
                        this.Target.GetComponent<Step3>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.設色 ? true : false;
                        //this.Target.GetComponent<Step4>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage. ? true : false;
                        //this.Target.GetComponent<Step5>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage. ? true : false;
                    }
                }

                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.放開;
                break;

            case MouseType.放開:
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
        isLock = false;
        //選取圖片後 關閉操作區 [0904功能取消]
        //this.Target.GetComponent<Step2>().enabled =
        //this.Target.GetComponent<Step3>().enabled = false;
        //this.Target.GetComponent<Step4>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化 ? false : true;
        //this.Target.GetComponent<Step5>().enabled = GameManager.script.CurrentDrawStage == GameManager.DrawStage.光源 ? false : true;
    }

    public void SetPictureStep2(GameObject ChangeObject)
    {
        if (Target.gameObject.name == "馬1")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬1_GUID);
            //ChangeObject.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬1_GUID);
            GameObject.Find("馬1-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬1_GUID);

        }
        if (Target.gameObject.name == "馬2")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬2_GUID);
            //ChangeObject.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬2_GUID);
            GameObject.Find("馬2-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬2_GUID);
        }
        if (Target.gameObject.name == "馬3")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬3_GUID);
            //ChangeObject.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬3_GUID);
            GameObject.Find("馬3-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.馬3_GUID);
        }
        if (Target.gameObject.name == "樹1")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹1_GUID);
            //ChangeObject.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹1_GUID);
            GameObject.Find("樹1-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹1_GUID);
        }
        if (Target.gameObject.name == "樹2")
        {
            this.Target.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹2_GUID);
            //ChangeObject.GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹2_GUID);
            GameObject.Find("樹2-明").GetComponent<SmoothMoves.Sprite>().SetTextureGUID(GameManager.script.樹2_GUID);
        }
    }


    /// <summary>
    /// 清空操作區 0904加入
    /// </summary>
    public void ClearControlArea()
    {
        if (Target)
        {
            this.Target.GetComponent<Step2>().enabled =
        this.Target.GetComponent<Step3>().enabled = false;
        }
    }



    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 放開 = 3
    }
}
