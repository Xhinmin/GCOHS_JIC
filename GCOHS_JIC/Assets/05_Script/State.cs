using UnityEngine;
using System.Collections;

public class State : MonoBehaviour
{
    public static State script;
    public GameObject 圖案物件;
    public GameObject 滑鼠的拖曳;
    public GameObject 滑鼠的點擊;
    private bool 明暗初始化 = false;
    private bool 設色初始化 = false;
    private bool 淡化初始化 = false;
    private bool 光源初始化 = false;
    public GameObject 明暗圖片區;
    public GameObject 設色圖片區;
    public GameObject 淡化圖片區;
    public GameObject 光源圖片區;
    public GameObject 明暗圖片區畫筆;
    public GameObject 設色圖片區畫筆;
    public GameObject 淡化圖片區畫筆;
    public GameObject 光源圖片區畫筆;
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

                foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    pi.isBlink = false;
                    pi.gameObject.GetComponent<SmoothMoves.Sprite>().color = new Color(1, 1, 1, 1);
                    pi.GetComponent<SmoothMoves.Sprite>().UpdateArrays();
                }
                滑鼠的拖曳.SetActive(false);
                滑鼠的點擊.SetActive(false);
                if (明暗圖片區)
                {
                    明暗圖片區.SetActive(false);
                    明暗圖片區畫筆.SetActive(false);
                }
                if (設色圖片區)
                {
                    設色圖片區.SetActive(false);
                    設色圖片區畫筆.SetActive(false);
                }
                if (淡化圖片區)
                {
                    淡化圖片區.SetActive(false);
                    淡化圖片區畫筆.SetActive(false);
                }
                if (光源圖片區)
                {
                    光源圖片區.SetActive(false);
                    光源圖片區畫筆.SetActive(false);
                }
                break;

            case GameManager.DrawStage.構圖:
                滑鼠的拖曳.SetActive(true);
                break;

            case GameManager.DrawStage.明暗:
                if (!明暗初始化)
                {
                    明暗初始化 = true;
                    滑鼠的點擊.SetActive(true);
                    明暗圖片區.SetActive(true);
                    明暗圖片區畫筆.SetActive(true);
                    //將馬跟樹閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (!pi.isUsed) pi.gameObject.SetActive(false);
                        else
                        {
                            if (pi.Type == PictureInfo.PictureType.馬 || pi.Type == PictureInfo.PictureType.樹)
                            {
                                pi.isUsed = false;
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
                    滑鼠的點擊.SetActive(true);
                    設色圖片區.SetActive(true);
                    設色圖片區畫筆.SetActive(true);
                    //將馬跟樹閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (!pi.isUsed) pi.gameObject.SetActive(false);
                        else
                        {
                            if (pi.Type == PictureInfo.PictureType.馬 || pi.Type == PictureInfo.PictureType.樹)
                            {
                                pi.isUsed = false;
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
                    滑鼠的點擊.SetActive(true);
                    淡化圖片區.SetActive(true);
                    淡化圖片區畫筆.SetActive(true);
                    //將土坡閃爍
                    foreach (var pi in 圖案物件.GetComponentsInChildren<PictureInfo>())
                    {
                        if (pi.isUsed)
                        {
                            if (pi.Type == PictureInfo.PictureType.土坡)
                            {
                                pi.isUsed = false;
                                pi.isBlink = true;
                                if (!pi.GetComponent<iTween>())
                                    iTween.ValueTo(pi.gameObject, iTween.Hash("name", "PickObject", "from", 1, "to", 0.2, "time", 0.5, "loopType", "pingPong", "onupdatetarget", this.gameObject, "onupdate", "changePictureAlpha"));

                            }
                        }
                    }
                }
                break;

            case GameManager.DrawStage.光源:
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
}