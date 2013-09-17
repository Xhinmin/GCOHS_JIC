using UnityEngine;
using System.Collections;

//狀態機

public class State : MonoBehaviour
{
    public static State script;
    public GameObject 圖案物件;

    private bool 明暗初始化 = false;
    private bool 設色初始化 = false;
    private bool 淡化初始化 = false;
    private bool 光源初始化 = false;

    public GameObject[] 影子;
    public GameObject 光源的控制桿;


    public GameObject 第一階段構圖操作區;
    public GameObject 第二階段明暗操作區;
    public GameObject 第三階段設色操作區;
    public GameObject 第四階段淡化操作區;
    public GameObject 第五階段光源操作區;
    public GameObject 操控區背景;
    public GameObject 第一階段構圖操控;
    public GameObject 第二三四階段明暗設色淡化操控;
    public GameObject 第五階段光源操控;

    private Transform[] brinkGameObjects;
    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.script.CurrentDrawStage)
        {
            case GameManager.DrawStage.等待中:

                //圖片閃爍中止
                foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    if (!pi.isUsed) pi.gameObject.SetActive(false);
                    pi.isBlink = false;
                    pi.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                    pi.GetComponent<SmoothMoves.Sprite>().UpdateArrays();
                }
                第一階段構圖操控.SetActive(false);
                第二三四階段明暗設色淡化操控.SetActive(false);

                第二階段明暗操作區.SetActive(false);
                第三階段設色操作區.SetActive(false);
                第四階段淡化操作區.SetActive(false);
                第五階段光源操作區.SetActive(false);
 

                if (ClickObject.script)
                {
                    ClickObject.script.HintAnimationisInit = true;
                    ClickObject.script.currentMouseType = ClickObject.MouseType.初始播放引導動畫;
                }

                //每階段動畫引導的初始化
                PlayHintBoneAnimation.script.animationType = PlayHintBoneAnimation.AnimationType.空動畫;
                PlayHandBoneAnimation.script.animationType = PlayHandBoneAnimation.AnimationType.空動畫;
              
                break;

            case GameManager.DrawStage.構圖:
                第一階段構圖操作區.SetActive(true);
                第一階段構圖操控.SetActive(true);
                break;

            case GameManager.DrawStage.明暗:

                if (!明暗初始化)
                {
                    明暗初始化 = true;
                    第二三四階段明暗設色淡化操控.SetActive(true);
                    第二階段明暗操作區.SetActive(true);
                    //將馬跟樹閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (!pi.isUsed) pi.gameObject.SetActive(false);
                        else
                        {
                            if (pi.Type == PictureInfo.PictureType.馬 || pi.Type == PictureInfo.PictureType.樹)
                            {
                                pi.isBlink = true;
                                if (!pi.GetComponent<iTween>())
                                    iTween.ValueTo(pi.gameObject, iTween.Hash("name", "PickObject", "from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdatetarget", this.gameObject, "onupdate", "changePictureAlpha"));

                            }
                        }
                    }
                }
                break;

            case GameManager.DrawStage.設色:

                if (!設色初始化)
                {
                    設色初始化 = true;
                    第二三四階段明暗設色淡化操控.SetActive(true);
                    第三階段設色操作區.SetActive(true);
                    //將馬跟樹閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (!pi.isUsed) pi.gameObject.SetActive(false);
                        else
                        {
                            if (pi.Type == PictureInfo.PictureType.馬 || pi.Type == PictureInfo.PictureType.樹)
                            {
                                pi.isBlink = true;
                                if (!pi.GetComponent<iTween>())
                                    iTween.ValueTo(pi.gameObject, iTween.Hash("name", "PickObject", "from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdatetarget", this.gameObject, "onupdate", "changePictureAlpha"));

                            }
                        }
                    }
                }
                break;

            case GameManager.DrawStage.淡化:
                if (!淡化初始化)
                {
                    淡化初始化 = true;
                    ClickObject.script.isLock = false;
                    第二三四階段明暗設色淡化操控.SetActive(true);
                    第四階段淡化操作區.SetActive(true);
                    //將土坡閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (pi.isUsed)
                        {
                            if (pi.Type == PictureInfo.PictureType.土坡)
                            {
                                pi.isBlink = true;
                                if (!pi.GetComponent<iTween>())
                                    iTween.ValueTo(pi.gameObject, iTween.Hash("name", "PickObject", "from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdatetarget", this.gameObject, "onupdate", "changePictureAlpha"));

                            }
                        }
                    }
                }
                break;

            case GameManager.DrawStage.光源:
                if (!光源初始化)
                {
                    光源初始化 = true;
                    第五階段光源操控.SetActive(true);
                    第五階段光源操作區.SetActive(true);
                    光源的控制桿.SetActive(true);
                    操控區背景.SetActive(false);
                    foreach (GameObject gameObject in 影子)
                    {
                        gameObject.SetActive(true);
                        iTween.ValueTo(光源的控制桿, iTween.Hash("name", "PickObject", "from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdatetarget", this.gameObject, "onupdate", "changePictureAlphaStep5"));
                    }
                }
                break;

            case GameManager.DrawStage.寄信:
                PlayHintBoneAnimation.script.animationType = PlayHintBoneAnimation.AnimationType.空動畫;
                break;
        }
    }

    void changePictureAlpha(float newValue)
    {
        foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
        {
            if (pi.isBlink)
            {
                pi.gameObject.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0.9f - newValue));
            }
        }
    }

    void changePictureAlphaStep5(float newValue)
    {
        光源的控制桿.gameObject.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 0.9f - newValue));
    }
}