using UnityEngine;
using System.Collections;

public class ObjectCollection : MonoBehaviour
{
    public GameObject NextButtonObject;

    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        int 使用馬樹數量 = 0;
        int 使用土坡數量 = 0;
        foreach (var script in this.GetComponentsInChildren<PictureInfo>())
        {
            if (script.Type == PictureInfo.PictureType.土坡)
            {
                if (script.isUsed)
                    使用土坡數量++;
            }
            else if (script.Type == PictureInfo.PictureType.馬樹)
            {
                if (script.isUsed)
                    使用馬樹數量++;
            }
        }

        if (使用馬樹數量 >= 1 & 使用土坡數量 == 3)
            this.NextButtonObject.SetActive(true);

        else
            this.NextButtonObject.SetActive(false);
    }
}
