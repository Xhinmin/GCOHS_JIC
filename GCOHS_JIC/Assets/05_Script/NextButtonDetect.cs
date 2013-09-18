using UnityEngine;
using System.Collections;

/// <summary>
/// 下一步按鈕的控制
/// </summary>
public class NextButtonDetect : MonoBehaviour
{
    public LayerMask TargetLayer;
    private SmoothMoves.BoneAnimation boneAnimation;

    void Start()
    {
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        this.boneAnimation.playAutomatically = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.script.CurrentDrawStage)
        {
            case GameManager.DrawStage.等待中:
            case GameManager.DrawStage.開頭動畫:
                break;


            case GameManager.DrawStage.構圖:
                this.boneAnimation.Play("下一步");
                break;
            case GameManager.DrawStage.明暗:
                this.boneAnimation.Play("下一步");
                break;
            case GameManager.DrawStage.設色:
                this.boneAnimation.Play("下一步");
                break;
            case GameManager.DrawStage.淡化:
                this.boneAnimation.Play("下一步");
                break;
            case GameManager.DrawStage.光源:
                this.boneAnimation.Play("下一步");
                break;
            case GameManager.DrawStage.寄信:
                this.boneAnimation.Play("寄出");
                break;
        }

        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), 100, this.TargetLayer))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                switch (GameManager.script.CurrentDrawStage)
                {
                    case GameManager.DrawStage.等待中:
                        break;
                    case GameManager.DrawStage.開頭動畫:
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.構圖);
                        break;
                    case GameManager.DrawStage.構圖:
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.明暗);
                        break;
                    case GameManager.DrawStage.明暗:
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.設色);

                        break;
                    case GameManager.DrawStage.設色:
                        //當轉到淡化階段時 由於設色可以不必要所有物件都做設定 ， 故所以在點 下一步時 將使用狀態=true
                        foreach (var pi in State.script.圖案物件.GetComponentsInChildren<PictureInfo>())
                            pi.isUsed = true;
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.淡化);
                        break;
                    case GameManager.DrawStage.淡化:
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.光源);
                        break;
                    case GameManager.DrawStage.光源:
                        //簡/繁體版不同功能

                        //簡體版(進入寄信)
                        if (GameManager.script.語言版本 == GameManager.Language.簡體)
                            GameManager.script.ChangeDrawStage(GameManager.DrawStage.寄信);
                        //繁體版(進入簽名)
                        else if (GameManager.script.語言版本 == GameManager.Language.繁體)
                            GameManager.script.ChangeDrawStage(GameManager.DrawStage.簽名);
                        break;
                    case GameManager.DrawStage.寄信:
                        SendEmail.script.RunSendEmail();
                        GameManager.script.ChangeDrawStage(GameManager.DrawStage.結束);
                        break;
                    case GameManager.DrawStage.簽名:
                        break;
                    case GameManager.DrawStage.列印:
                        break;
                }
            }
        }
    }
}
