using UnityEngine;
using System.Collections;

public class PlayHandBoneAnimation : MonoBehaviour
{

    public static PlayHandBoneAnimation script;

    // Use this for initialization
    private SmoothMoves.BoneAnimation boneAnimation;

    public Vector3 拖曳引導位置;
    public Vector3 指向引導位置_操作區潑墨;
    public Vector3 指向引導位置_操作區物件;
    public Vector3 指向引導位置_土坡類;
    public Vector3 指向引導位置_馬樹類;
    public Vector3 太陽引導位置;
    public AnimationType animationType;
    void Start()
    {
        script = this;
        boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();
        boneAnimation.playAutomatically = false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (animationType)
        {
            case AnimationType.拖曳引導:
                this.transform.position = 拖曳引導位置;
                boneAnimation.Play(AnimationType.拖曳引導.ToString());
                break;
            case AnimationType.指向引導_操作區潑墨:
                this.transform.position = 指向引導位置_操作區潑墨;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                boneAnimation.Play("指向引導");
                break;
            case AnimationType.指向引導_操作區物件:
                this.transform.position = 指向引導位置_操作區物件;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                boneAnimation.Play("指向引導");
                break;
            case AnimationType.指向引導_土坡類:
                this.transform.position = 指向引導位置_土坡類;
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                boneAnimation.Play("指向引導");
                break;
            case AnimationType.指向引導_馬樹類:
                this.transform.position = 指向引導位置_馬樹類;
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                boneAnimation.Play("指向引導");
                break;
            case AnimationType.太陽引導:
                this.transform.position = 太陽引導位置;
                boneAnimation.Play(AnimationType.太陽引導.ToString());

                break;
            case AnimationType.空動畫:
                boneAnimation.Play(AnimationType.空動畫.ToString());
                break;

        }

    }


    public enum AnimationType
    {
        拖曳引導 = 0,
        指向引導_操作區潑墨 = 1,
        指向引導_操作區物件 = 2,
        指向引導_土坡類 = 3,
        指向引導_馬樹類= 4,
        太陽引導 = 5,

        空動畫 = 6
    }
}
