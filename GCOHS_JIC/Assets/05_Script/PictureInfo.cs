using UnityEngine;
using System.Collections;

/// <summary>
/// 設定物件的一些資訊(在作畫區最小、最大的尺寸)
/// </summary>
public class PictureInfo : MonoBehaviour
{
    public PictureType Type;
    public bool CanMove;
    public bool CanPick;
    public bool isUsed;
    public bool isBlink;
    public float MixScale;
    public float MaxScale;

    public float MixDepth;
    public float MaxDepth;

    private Vector3 originPosition;
    private Vector3 originScale;

    public void ChangeScaleDepth()
    {
        if (this.transform.position.x < GameManager.script.scaleLineTop.position.x)
            return;

        float value = Mathf.Clamp(this.transform.position.y, GameManager.script.scaleLineBottom.position.y, GameManager.script.scaleLineTop.position.y);
        float scale = Mathf.Abs(value - GameManager.script.scaleLineTop.position.y) / Mathf.Abs(GameManager.script.scaleLineBottom.position.y - GameManager.script.scaleLineTop.position.y);
        float pictureScale = Mathf.Lerp(this.MixScale, this.MaxScale, scale);
        float pictureDepth = Mathf.Lerp(this.MixDepth, this.MaxDepth, 1 - scale);
        this.transform.localScale = new Vector3(pictureScale, pictureScale);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, pictureDepth);
    }

    void Start()
    {
        this.isUsed = false;
        this.originPosition = this.transform.position;
        this.originScale = this.transform.localScale;
    }

    public void BacktoOriginPosition()
    {
        this.transform.position = this.originPosition;
        this.transform.localScale = this.originScale;
        this.isUsed = false;
    }

    public enum PictureType
    {
        未定義 = 0, 馬樹 = 1, 土坡 = 2
    }
}