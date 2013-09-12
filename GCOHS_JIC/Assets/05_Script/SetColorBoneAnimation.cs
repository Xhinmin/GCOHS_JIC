using UnityEngine;
using System.Collections;

//設色階段中 在操作區 下方物件的BoneAnimation
//用於變化圖片 但不作用在Sprite 所以右方實際畫布區是必須額外寫Sprite的變化圖片

public class SetColorBoneAnimation : MonoBehaviour
{
    public static SetColorBoneAnimation script;
    public PictureType pictureType;
    // Use this for initialization
    private SmoothMoves.BoneAnimation boneAnimation;

    void Start()
    {
        script = this;
        boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pictureType)
        {
            case PictureType.空:
                boneAnimation.Play("空",PlayMode.StopAll);
                break;
            case PictureType.馬1顏色1:
                boneAnimation.Play("馬1顏色1", PlayMode.StopAll);
                break;
            case PictureType.馬1顏色2:
                boneAnimation.Play("馬1顏色2", PlayMode.StopAll);
                break;
            case PictureType.馬1顏色3:
                boneAnimation.Play("馬1顏色3", PlayMode.StopAll);
                break;
            case PictureType.馬2顏色1:
                boneAnimation.Play("馬2顏色1", PlayMode.StopAll);
                break;
            case PictureType.馬2顏色2:
                boneAnimation.Play("馬2顏色2", PlayMode.StopAll);
                break;
            case PictureType.馬2顏色3:
                boneAnimation.Play("馬2顏色3", PlayMode.StopAll);
                break;
            case PictureType.馬3顏色1:
                boneAnimation.Play("馬3顏色1", PlayMode.StopAll);
                break;
            case PictureType.馬3顏色2:
                boneAnimation.Play("馬3顏色2", PlayMode.StopAll);
                break;
            case PictureType.馬3顏色3:
                boneAnimation.Play("馬3顏色3", PlayMode.StopAll);
                break;
            case PictureType.樹1顏色1:
                boneAnimation.Play("樹1顏色1", PlayMode.StopAll);
                break;
            case PictureType.樹1顏色2:
                boneAnimation.Play("樹1顏色2", PlayMode.StopAll);
                break;
            case PictureType.樹1顏色3:
                boneAnimation.Play("樹1顏色3", PlayMode.StopAll);
                break;
            case PictureType.樹2顏色1:
                boneAnimation.Play("樹2顏色1", PlayMode.StopAll);
                break;
            case PictureType.樹2顏色2:
                boneAnimation.Play("樹2顏色2", PlayMode.StopAll);
                break;
            case PictureType.樹2顏色3:
                boneAnimation.Play("樹2顏色3", PlayMode.StopAll);
                break;
        }
    }


    public enum PictureType
    {
        空 = 0,
        馬1顏色1 = 1, 馬1顏色2 = 2, 馬1顏色3 = 3,
        馬2顏色1 = 4, 馬2顏色2 = 5, 馬2顏色3 = 6,
        馬3顏色1 = 7, 馬3顏色2 = 8, 馬3顏色3 = 9,
        樹1顏色1 = 10, 樹1顏色2 = 11, 樹1顏色3 = 12,
        樹2顏色1 = 13, 樹2顏色2 = 14, 樹2顏色3 = 15
    }
}
