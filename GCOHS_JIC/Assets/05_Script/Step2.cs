using UnityEngine;
using System.Collections;

// 第二步驟 : 明暗
public class Step2 : MonoBehaviour
{
    public GameObject 潑墨;
    public GameObject 明圖;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }

    void OnEnable()
    {
        print(gameObject.name + "第二步驟的功能被開啟了");
        if (潑墨) 潑墨.SetActive(true);
        if (明圖) 明圖.SetActive(true);
    }

    void OnDisable()
    {
        if (潑墨) 潑墨.SetActive(false);
        if (明圖) 明圖.SetActive(false);
    }
}
