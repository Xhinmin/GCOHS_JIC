﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-09-02
/// Author：Ian
/// Description：
///     自動刪除BoneAnimation(觸發UserTrigger)
///     0902新增：根據不同動畫播完後，切換不同的狀態
/// </summary>
public class AutoDestroyBoneAnimation : MonoBehaviour
{
    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.RegisterUserTriggerDelegate(AutoDestroy);
    }

    /// <summary>
    /// SmoothMove UserTrigger(當播完動畫後刪除自己)
    /// </summary>
    /// <param name="triggerEvent"></param>
    public void AutoDestroy(SmoothMoves.UserTriggerEvent triggerEvent)
    {
        //刪除自己，同時切換狀態
        switch (triggerEvent.animationName)
        {
            case "構圖":
                GameManager.script.CurrentDrawStage = GameManager.DrawStage.構圖;
                GameManager.script.ShowStageHint(GameManager.DrawStage.構圖);
                GameManager.script.物件區背景.SetActive(true);
                break;

            case "明暗":
                GameManager.script.CurrentDrawStage = GameManager.DrawStage.明暗;
                GameManager.script.ShowStageHint(GameManager.DrawStage.明暗);
                GameManager.script.物件區背景.SetActive(true);
                break;

            case "設色":
                GameManager.script.CurrentDrawStage = GameManager.DrawStage.設色;
                GameManager.script.ShowStageHint(GameManager.DrawStage.設色);
                GameManager.script.物件區背景.SetActive(true);
                break;

            case "淡化":
                GameManager.script.CurrentDrawStage = GameManager.DrawStage.淡化;
                GameManager.script.ShowStageHint(GameManager.DrawStage.淡化);
                GameManager.script.物件區背景.SetActive(true);
                break;

            case "光源":
                GameManager.script.CurrentDrawStage = GameManager.DrawStage.光源;
                GameManager.script.ShowStageHint(GameManager.DrawStage.光源);
                GameManager.script.物件區背景.SetActive(true);
                break;
        }

        Destroy(this.gameObject);
    }
}
