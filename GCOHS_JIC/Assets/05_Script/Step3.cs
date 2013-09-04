using UnityEngine;
using System.Collections;

// 第三步驟 : 設色
public class Step3 : MonoBehaviour
{
    public GameObject 畫筆;
    public GameObject 顏色一;
    public GameObject 顏色二;
    public GameObject 顏色三;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }

    void OnEnable()
    {
        print(gameObject.name + "第三步驟的功能被開啟了");
        if (畫筆) 畫筆.SetActive(true);
        if (顏色一) 顏色一.SetActive(true);
        if (顏色二) 顏色二.SetActive(true);
        if (顏色三) 顏色三.SetActive(true);
    }


    void OnDisable()
    {
        if (顏色一) 顏色一.SetActive(false);
        if (顏色二) 顏色二.SetActive(false);
        if (顏色三) 顏色三.SetActive(false);
    }

}
