using UnityEngine;
using System.Collections;

//提供給　操作區　的貓斯射線
//點擊後　直接呼叫　"ClickObject" 的　Target　裡面改　SmoothMove的圖

public class MouseRaycast : MonoBehaviour
{

    public static MouseRaycast script;
    public Camera ViewCamera;
    private RaycastHit hit;
    public GameObject MouseTarget;
    public PictureType pictureType;
    public SetColorBoneAnimation.PictureType 潑墨顏色;
    public bool isBlink;


    //九月九號新增 當畫筆被點擊後 才能開啟變色功能
    private static bool 是否可以對操作區的物件上色 = false;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<SmoothMoves.Sprite>())
        {
            switch (pictureType)
            {
                // 處理潑墨部分的閃爍
                case MouseRaycast.PictureType.潑墨:
                    if (!GetComponent<iTween>() && this.isBlink)
                    {
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                        iTween.ValueTo(this.gameObject, iTween.Hash("from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdate", "changePictureAlpha"));
                    }
                    else if (!this.isBlink)
                    {
                        iTween.Stop(this.gameObject);
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().UpdateArrays();
                    }
                    break;

                // 處理操作區物件的閃爍
                case MouseRaycast.PictureType.操作區物件:

                    if (!GetComponent<iTween>() && 是否可以對操作區的物件上色)
                    {
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                        iTween.ValueTo(this.gameObject, iTween.Hash("from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdate", "changePictureAlpha"));
                    }
                    else if (!是否可以對操作區的物件上色)
                    {
                        iTween.Stop(this.gameObject);
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                        this.gameObject.GetComponent<SmoothMoves.Sprite>().UpdateArrays();
                    }
                    break;
            }
        }

        if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 1000))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                {
                    MouseTarget = hit.transform.gameObject;


                    if (pictureType == PictureType.潑墨)
                    {
                        if (isBlink)
                        {
                            isBlink = false;
                            //將其他潑墨閃爍
                            if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.設色)
                            {

                                //存下目前要設定的顏色
                                GameManager.script.設色潑墨顏色 = this.潑墨顏色;

                                foreach (Transform child in this.transform.parent.parent)
                                {
                                    foreach (Transform innerchild in child)
                                    {
                                        if (innerchild != this.transform)
                                            innerchild.gameObject.GetComponent<MouseRaycast>().isBlink = true;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Transform child in this.transform.parent)
                                {
                                    if (child != this.transform)
                                        child.gameObject.GetComponent<MouseRaycast>().isBlink = true;
                                }
                            }
                            是否可以對操作區的物件上色 = true;
                        }
                    }

                    if (pictureType == PictureType.操作區物件)
                    {
                        if (是否可以對操作區的物件上色)
                        {
                            if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗)
                            {
                                ClickObject.script.SetPictureStep2(MouseTarget);
                                是否可以對操作區的物件上色 = false;
                            }
                            if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.設色)
                            {
                                //改變操作區操作物件的圖（Animation）
                                 
                                SetColorBoneAnimation.script.pictureType = GameManager.script.設色潑墨顏色;
                                GameManager.script.設色潑墨顏色 = SetColorBoneAnimation.script.pictureType;

                                //改變實際操作物件的圖（Sprite）
                                ClickObject.script.SetPictureStep3(MouseTarget);
                                是否可以對操作區的物件上色 = false;
                            }
                            if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化)
                            {
                                ClickObject.script.SetPictureStep4();
                                是否可以對操作區的物件上色 = false;
                            }
                        }
                    }
                }

            }
        }
    }

    void changePictureAlpha(float newValue)
    {
        this.gameObject.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0.9f - newValue));
    }

    void OnEnable()
    {
        this.isBlink = true;
    }

    public enum PictureType
    {
        潑墨 = 0, 操作區物件 = 1
    }
}
