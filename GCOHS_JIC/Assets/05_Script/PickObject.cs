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

    public enum MouseType { 點擊, 拖曳中, 放開 };
    public MouseType mouseType;

    private float clickTime;
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
            this.Target = null;

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseType = MouseType.點擊;
        }
        else
        {
            clickTime = 0;
            mouseType = MouseType.放開;
        }


        if (mouseType == MouseType.點擊)
        {
            iTween.ValueTo(this.Target.gameObject, iTween.Hash(
"from", 0,
"to", 255,
"time", 1,
"onupdate", "changeMotionBlur"
));

            clickTime += Time.deltaTime;
            if (clickTime > 0.5F)
            {
                mouseType = MouseType.拖曳中;
                if (Target) print(Target.transform.name + " 已被選取");
            }
        }

        if (mouseType == MouseType.拖曳中)
        {
            if (Target)
                Target.transform.position = new Vector3(this.camera.ScreenToWorldPoint(Input.mousePosition).x,
                    this.camera.ScreenToWorldPoint(Input.mousePosition).y, Target.transform.position.z);
        }

        if (mouseType == MouseType.放開)
        {
            this.Target = this.hit.transform.gameObject;


        }

    }

    void changeMotionBlur(float newValue)
    {
        Target.GetComponent<SmoothMoves.Sprite>().color.a = newValue;
    }
}
