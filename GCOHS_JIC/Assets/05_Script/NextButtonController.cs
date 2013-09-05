using UnityEngine;
using System.Collections;

public class NextButtonController : MonoBehaviour
{
    private GameObject NextButtonObject;

    // Use this for initialization
    void Start()
    {
        this.NextButtonObject = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.script.CurrentDrawStage)
        {
            case GameManager.DrawStage.等待中:
                this.NextButtonObject.SetActive(false);
                break;

            case GameManager.DrawStage.構圖:
                int 使用馬數量 = 0;
                int 使用樹數量 = 0;
                int 使用土坡數量 = 0;
                foreach (var script in State.script.圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    if (script.Type == PictureInfo.PictureType.土坡)
                    {
                        if (script.isUsed)
                            使用土坡數量++;
                    }
                    else if (script.Type == PictureInfo.PictureType.馬)
                    {
                        if (script.isUsed)
                            使用馬數量++;
                    }
                    else if (script.Type == PictureInfo.PictureType.樹)
                    {
                        if (script.isUsed)
                            使用樹數量++;
                    }
                }

                if (使用馬數量 >= 1 & 使用樹數量 >= 1 & 使用土坡數量 == 3)
                    this.NextButtonObject.SetActive(true);

                else
                    this.NextButtonObject.SetActive(false);
                break;

            case GameManager.DrawStage.明暗:
                bool isCheck1 = true;
                foreach (var script in State.script.圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    if (script.Type == PictureInfo.PictureType.馬 || script.Type == PictureInfo.PictureType.樹)
                    {
                        if (!script.isUsed)
                        {
                            isCheck1 = false;
                            break;
                        }
                    }
                }
                this.NextButtonObject.SetActive(isCheck1);

                break;

            case GameManager.DrawStage.設色:
                bool isCheck2 = false;
                foreach (var script in State.script.圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    if (script.Type == PictureInfo.PictureType.馬 || script.Type == PictureInfo.PictureType.樹)
                    {
                        if (script.isUsed)
                        {
                            isCheck2 = true;
                            break;
                        }
                    }
                }
                this.NextButtonObject.SetActive(isCheck2);
                break;

            case GameManager.DrawStage.淡化:
                bool isCheck3 = true;
                foreach (var script in State.script.圖案物件.GetComponentsInChildren<PictureInfo>())
                {
                    if (script.Type == PictureInfo.PictureType.土坡)
                    {
                        if (!script.isUsed)
                        {
                            isCheck3 = false;
                            break;
                        }
                    }
                }
                this.NextButtonObject.SetActive(isCheck3);
                break;

            case GameManager.DrawStage.光源:
                break;
        }


    }
}
