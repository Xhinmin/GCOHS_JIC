using UnityEngine;
using System.Collections;

public class PlayHintBoneAnimation : MonoBehaviour
{

    public static SetColorBoneAnimation script;

    // Use this for initialization
    private SmoothMoves.BoneAnimation boneAnimation;

    public AnimationType animationType;
    void Start()
    {
        boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (animationType)
        {
            case AnimationType.畫布上方太陽:
                boneAnimation.Play(AnimationType.畫布上方太陽.ToString());
                break;
            case AnimationType.畫布閃爍圖片:
                boneAnimation.Play(AnimationType.畫布閃爍圖片.ToString());
                break;
            case AnimationType.操作閃爍圖片:
                boneAnimation.Play(AnimationType.操作閃爍圖片.ToString());
                break;
            case AnimationType.操作閃爍潑墨:
                boneAnimation.Play(AnimationType.操作閃爍潑墨.ToString());
                break;
            case AnimationType.空動畫:
                boneAnimation.Play(AnimationType.空動畫.ToString());
                break;

        }

    }


    public enum AnimationType
    {
        畫布閃爍圖片 = 0,
        操作閃爍潑墨 = 1,
        操作閃爍圖片 = 2,
        畫布上方太陽 = 3,
        空動畫 = 4
    }
}
