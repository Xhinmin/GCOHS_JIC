using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-09-24
/// Author：Ian
/// Description：
///     標題動畫控制器
///     0902新增：根據不同動畫播完後，切換不同的狀態
///     0924新增：修改刪除方式，播完動畫後偵測使用者是否觸碰畫面
/// </summary>
public class TitleAnimationController : MonoBehaviour
{
    private SmoothMoves.BoneAnimation boneAnimation;

    private bool checkSkip = false;
    private GameManager.DrawStage changeStage;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(this.AutoDestroy);
    }

    void Update()
    {
        //開頭動畫狀態進行觸碰事件偵測
        if (GameManager.script.CurrentDrawStage == GameManager.DrawStage.開頭動畫)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.script.ChangeDrawStage(GameManager.DrawStage.構圖);   //進入構圖動畫
                GameManager.script.現代郎世寧.SetActive(true);       //開啟右上"現代郎世寧"
                Destroy(this.gameObject);
            }
        }

        if (this.checkSkip)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.script.CurrentDrawStage = this.changeStage;
                GameManager.script.ShowStageHint(this.changeStage);
                GameManager.script.物件區背景.SetActive(true);
                this.checkSkip = false;
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完動畫後開啟偵測模式)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void AutoDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //開啟偵測模式
        switch (triggerEvent.animationName)
        {
            case "構圖":
                this.changeStage = GameManager.DrawStage.構圖;
                this.checkSkip = true;
                break;

            case "明暗":
                this.changeStage = GameManager.DrawStage.明暗;
                this.checkSkip = true;
                break;

            case "設色":
                this.changeStage = GameManager.DrawStage.設色;
                this.checkSkip = true;
                break;

            case "淡化":
                this.changeStage = GameManager.DrawStage.淡化;
                this.checkSkip = true;
                break;

            case "光源":
                this.changeStage = GameManager.DrawStage.光源;
                this.checkSkip = true;
                break;
        }
    }
}