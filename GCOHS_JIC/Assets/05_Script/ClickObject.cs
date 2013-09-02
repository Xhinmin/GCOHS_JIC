using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第二步驟：明暗
/// </summary>
/// 
public class ClickObject : MonoBehaviour
{
    public Camera ViewCamera;
    public LayerMask TargetLayer;
    private GameObject Target;
    private RaycastHit hit;
    private MouseType currentMouseType;
    // Use this for initialization
    void Start()
    {

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
                {
                    this.currentMouseType = MouseType.點擊;
                }
                break;
            case MouseType.點擊:
                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer))
                {
                    if (Target)
                    {
                        //如果當步驟二設定好後 才能賦予新的Target
                        if (Target.GetComponent<Step2>().isDone)
                            this.Target = this.hit.transform.gameObject;
                    }
                    else
                        this.Target = this.hit.transform.gameObject;
                    
                    //如果不可以被選取
                    if (!this.Target.GetComponent<PictureInfo>().CanPick)
                    {
                        this.currentMouseType = MouseType.無狀態;
                        return;
                    }
                    else
                    {
                        this.Target.GetComponent<PictureInfo>().isBlink = false;
                        this.Target.GetComponent<SmoothMoves.Sprite>().SetColor(new Color(1, 1, 1, 1));
                        //點擊後開始選明暗
                        //選完後要把Target.GetComponent<Step2>().isDone = true;
                        this.Target.GetComponent<Step2>().enabled = true;
                    }
                }

                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.放開;
                break;

            case MouseType.放開:
                this.currentMouseType = MouseType.無狀態;
                break;
        }

        
    }
    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 放開 = 3
    }
}
