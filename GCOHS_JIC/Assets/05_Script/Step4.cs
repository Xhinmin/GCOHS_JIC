using UnityEngine;
using System.Collections;

// 第四步驟 : 淡化
public class Step4 : MonoBehaviour
{
    public GameObject 潑墨;
    public GameObject 淡化圖;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }

    void OnEnable()
    {
        print(gameObject.name + "第四步驟的功能被開啟了");
        if (潑墨) 潑墨.SetActive(true);
        if (淡化圖) 淡化圖.SetActive(true);
    }


    void OnDisable()
    {
        if (潑墨) 潑墨.SetActive(false);
        if (淡化圖) 淡化圖.SetActive(false);
    }

}
