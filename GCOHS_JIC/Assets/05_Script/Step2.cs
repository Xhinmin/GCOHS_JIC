using UnityEngine;
using System.Collections;

// 第二步驟 : 明暗
public class Step2 : MonoBehaviour
{
    public GameObject 畫筆;
    public GameObject 明圖;
    //public GameObject 暗圖;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }
    // Use this for initialization
    void Start()
    {
    }


    void OnEnable()
    {
        print(gameObject.name + "第二步驟的功能被開啟了");
        if (畫筆) 畫筆.SetActive(true);
        if (明圖) 明圖.SetActive(true);
    }

    void OnDisable()
    {
        if (明圖) 明圖.SetActive(false);
    }
}
