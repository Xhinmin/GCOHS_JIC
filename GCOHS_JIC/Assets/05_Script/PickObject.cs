using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第一步驟：選取圖片
/// </summary>
/// 
public class PickObject : MonoBehaviour
{
    public GameObject Target;

    public Camera camera;

    public LayerMask TargetLayer;

    private RaycastHit hit;

    //滑鼠狀態
    public enum MouseType { 點擊, 拖曳中, 放開 };
    public MouseType mouseType;
    //按下累積時間
    private float clickTime;
    //按下累積時間門檻
    public float clickTime_threshold;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer) == true)
        {
            ProcessMouseState();
        }
        else
        {
            //當超出範圍時(例如滑鼠移動太快，圖片沒跟到的時候) 要把圖片ALPHA調回１
            mouseType = MouseType.放開;
            iTween.StopByName("PickObject");
            if (Target) Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1)); 
            this.Target = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1) * 100);
    }


    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        // 狀態:點擊 滑鼠按下時間>門檻時 改變狀態為拖曳
        if (mouseType == MouseType.點擊)
        {
            clickTime += Time.deltaTime;
            if (clickTime > clickTime_threshold)
            {
                mouseType = MouseType.拖曳中;
                if (Target) print(Target.transform.name + " 已被選取");
            }
        }
        //狀態：拖曳　改變XY座標　當滑鼠放開時　狀態：放開
        if (mouseType == MouseType.拖曳中)
        {
            if (Target)
            {
                Target.transform.position = new Vector3(this.camera.ScreenToWorldPoint(Input.mousePosition).x,
                    this.camera.ScreenToWorldPoint(Input.mousePosition).y, Target.transform.position.z);

                if (!this.GetComponent<iTween>())
                    iTween.ValueTo(this.gameObject, iTween.Hash(
                        "name", "PickObject",
                        "from", 1,
                        "to", 0.25F,
                        "onupdate", "changePictureAlpha",
                        "loopType", "pingPong",
                        "time", 1
                        ));
            }


            if (!Input.GetKey(KeyCode.Mouse0))
            {
                mouseType = MouseType.放開;
            }
        }

        //狀態：放開　當滑鼠點擊時　狀態：點擊
        if (mouseType == MouseType.放開)
        {
            this.Target = this.hit.transform.gameObject;

            iTween.StopByName("PickObject");
            Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1));
            clickTime = 0;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                mouseType = MouseType.點擊;
            }
        }

    }

    void changePictureAlpha(float newValue)
    {
        if (Target)
            Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, newValue));
    }
}
