using UnityEngine;
using System.Collections;

//給光源用的滑鼠拖曳
public class ClickObjectStep5 : MonoBehaviour
{
    public static ClickObjectStep5 script;
    public Camera ViewCamera;
    public LayerMask TargetLayer;
    private GameObject Target;
    private RaycastHit hit;
    public MouseType currentMouseType;

    public int Center = -510;
    public int Max = -620;
    public int Min = -400;
    //是否可以選擇下一個　物件　的鎖定狀態
    public bool isLock;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {

        ProcessMouseState();
    }

    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        switch (this.currentMouseType)
        {
            case MouseType.無狀態:
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.點擊;
                break;

            case MouseType.點擊:
                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                {
                    this.currentMouseType = MouseType.拖曳中;
                }
                else
                    this.currentMouseType = MouseType.無狀態;
                break;

            case MouseType.拖曳中:

                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                {
                    //中心點X = -510 間隔 110
                    this.hit.transform.position = new Vector3(
                       Mathf.Max(Mathf.Min(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).x, Min), Max),
                       this.hit.transform.position.y,
                       this.hit.transform.position.z);

                    foreach (GameObject gameObject in State.script.影子)
                    {
                        if (gameObject.activeInHierarchy)
                        {
                            gameObject.transform.localScale = new Vector3(
                                Mathf.Lerp(2,1,Mathf.Abs(this.hit.transform.position.x - Center) / 110F),
                                gameObject.transform.localScale.y,
                                gameObject.transform.localScale.z);

                            gameObject.transform.localPosition = new Vector3(
                                    Mathf.Lerp(
                                    150,
                                    -150,
                                     (((this.hit.transform.position.x - Center) / 110F) + 1) / 2F
                                    ),
                                gameObject.transform.localPosition.y,
                                gameObject.transform.localPosition.z);
                        }
                    }

                }
                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.無狀態;

                break;

            case MouseType.放開:

                this.currentMouseType = MouseType.無狀態;
                break;
        }


    }




    /// <summary>
    /// 清空操作區 0904加入
    /// </summary>
    public void ClearControlArea()
    {
        if (Target)
        {
            if (this.Target.GetComponent<Step2>()) this.Target.GetComponent<Step2>().enabled = false;
            if (this.Target.GetComponent<Step3>()) this.Target.GetComponent<Step3>().enabled = false;
            if (this.Target.GetComponent<Step4>()) this.Target.GetComponent<Step4>().enabled = false;

        }
    }



    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 拖曳中 = 2, 放開 = 3
    }
}