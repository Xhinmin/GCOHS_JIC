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

    public float clickTime_threshold;
    // Use this for initialization
    void Start()
    {
        mouseType = MouseType.放開;
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


        if (mouseType == MouseType.點擊)
        {
            clickTime += Time.deltaTime;
            if (clickTime > clickTime_threshold)
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

            if (!Input.GetKey(KeyCode.Mouse0))
            {
                mouseType = MouseType.放開;
            }
        }

        if (mouseType == MouseType.放開)
        {
            this.Target = this.hit.transform.gameObject;
            clickTime = 0;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                mouseType = MouseType.點擊;
            }
        }

    }

    void changePictureAlpha(float newValue)
    {
        print(newValue);
        if (Target)
            Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, newValue));
    }
}
