using UnityEngine;
using System.Collections;

//提供給　操作區　的貓斯射線
//點擊後　直接呼叫　"ClickObject" 的　Target　裡面改　SmoothMove的圖

public class MouseRaycast : MonoBehaviour
{

    public static MouseRaycast script;
    public Camera ViewCamera;
    private RaycastHit hit;
    public GameObject MouseTarget;
        // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 1000))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                {
                    MouseTarget = hit.transform.gameObject;

                    if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.設色)
                        ClickObject.script.SetPictureStep3(MouseTarget);

                    if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.明暗)
                        ClickObject.script.SetPictureStep2(MouseTarget);

                    if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.淡化)
                        ClickObject.script.SetPictureStep4();
                }
                
            }
        }
    }
}
