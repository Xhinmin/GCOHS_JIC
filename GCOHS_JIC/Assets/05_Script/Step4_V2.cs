using UnityEngine;
using System.Collections;

// 第四步驟第二版: 淡化
public class Step4_V2 : MonoBehaviour
{
    public GameObject 操作拉霸區;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }

    void OnEnable()
    {
        print(gameObject.name + "第四步驟的功能被開啟了");
        if (操作拉霸區) 操作拉霸區.SetActive(true);
    }


    void OnDisable()
    {
        if (操作拉霸區) 操作拉霸區.SetActive(false);
    }

}
