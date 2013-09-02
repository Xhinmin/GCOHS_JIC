using UnityEngine;
using System.Collections;

/// <summary>
/// 下一步按鈕的控制
/// </summary>
public class NextButton : MonoBehaviour
{
    public Camera ViewCamera;
    public LayerMask TargetLayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), 100, this.TargetLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.script.CurrentDrawStage != GameManager.DrawStage.等待中)
                    GameManager.script.ChangeDrawStage(GameManager.script.CurrentDrawStage + 1);
            }
        }
    }
}
