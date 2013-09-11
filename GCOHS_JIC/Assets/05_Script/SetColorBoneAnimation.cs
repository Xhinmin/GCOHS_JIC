using UnityEngine;
using System.Collections;

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
                boneAnimation.Play("空");
                break;
            case PictureType.馬1顏色1:
                boneAnimation.Play("馬1顏色1");
                break;
            case PictureType.馬1顏色2:
                boneAnimation.Play("馬1顏色2");
                break;
            case PictureType.馬1顏色3:
                boneAnimation.Play("馬1顏色3");
                break;
            case PictureType.馬2顏色1:
                boneAnimation.Play("馬2顏色1");
                break;
            case PictureType.馬2顏色2:
                boneAnimation.Play("馬2顏色2");
                break;
            case PictureType.馬2顏色3:
                boneAnimation.Play("馬2顏色3");
                break;
            case PictureType.馬3顏色1:
                boneAnimation.Play("馬3顏色1");
                break;
            case PictureType.馬3顏色2:
                boneAnimation.Play("馬3顏色2");
                break;
            case PictureType.馬3顏色3:
                boneAnimation.Play("馬3顏色3");
                break;
            case PictureType.樹1顏色1:
                boneAnimation.Play("樹1顏色1");
                break;
            case PictureType.樹1顏色2:
                boneAnimation.Play("樹1顏色2");
                break;
            case PictureType.樹1顏色3:
                boneAnimation.Play("樹1顏色3");
                break;
            case PictureType.樹2顏色1:
                boneAnimation.Play("樹2顏色1");
                break;
            case PictureType.樹2顏色2:
                boneAnimation.Play("樹2顏色2");
                break;
            case PictureType.樹2顏色3:
                boneAnimation.Play("樹2顏色3");
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
