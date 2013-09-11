using UnityEngine;
using System.Collections;

// 第三步驟 : 設色
public class Step3 : MonoBehaviour
{
    public SetColorBoneAnimation.PictureType pictureType;
    public GameObject 潑墨;
    public GameObject 顏色一;
    public GameObject 顏色二;
    public GameObject 顏色三;
    public GameObject 操作區物件;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }

    void OnEnable()
    {
        print(gameObject.name + "第三步驟的功能被開啟了");
        if (潑墨) 潑墨.SetActive(true);
        if (操作區物件) 操作區物件.SetActive(true);
        if (顏色一) 顏色一.SetActive(true);
        if (顏色二) 顏色二.SetActive(true);
        if (顏色三) 顏色三.SetActive(true);
    }


    void OnDisable()
    {
        if (潑墨) 潑墨.SetActive(false);
        if (操作區物件) 操作區物件.SetActive(false);
        if (顏色一) 顏色一.SetActive(false);
        if (顏色二) 顏色二.SetActive(false);
        if (顏色三) 顏色三.SetActive(false);
    }

}
