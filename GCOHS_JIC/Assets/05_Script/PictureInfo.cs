using UnityEngine;
using System.Collections;

/// <summary>
/// 設定物件的一些資訊(在作畫區最小、最大的尺寸)
/// </summary>
public class PictureInfo : MonoBehaviour
{
    public float MixScale;
    public float MaxScale;

    private Vector3 originPosition;
    private Vector3 originScale;

    public void ChangeScale()
    {
        if (this.transform.position.x < GameManager.script.scaleLineTop.position.x)
            return;

        float value = Mathf.Clamp(this.transform.position.y, GameManager.script.scaleLineBottom.position.y, GameManager.script.scaleLineTop.position.y);
        float scale = Mathf.Abs(value - GameManager.script.scaleLineTop.position.y) / Mathf.Abs(GameManager.script.scaleLineBottom.position.y - GameManager.script.scaleLineTop.position.y);
        float pictureScale = Mathf.Lerp(this.MixScale, this.MaxScale, scale);
        this.transform.localScale = new Vector3(pictureScale, pictureScale);
    }

    void Start()
    {
        this.originPosition = this.transform.position;
        this.originScale = this.transform.localScale;
    }

    public void BacktoOriginPosition()
    {
        this.transform.position = this.originPosition;
        this.transform.localScale = this.originScale;
    }
}