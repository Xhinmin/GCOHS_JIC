using UnityEngine;
using System.Collections;

// 第二步驟 : 明暗
public class Step2 : MonoBehaviour {

    public GameObject 明圖;
    public GameObject 暗圖;
    public bool isDone;

    void Awake()
    {
        if (this.enabled)
            this.enabled = false;
    }
	// Use this for initialization
	void Start () {
        print("第二步驟的功能被開啟了");
        if(明圖)  明圖.SetActive(true);
        if(暗圖)  暗圖.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
