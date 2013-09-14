using UnityEngine;
using System.Collections;

//【第五階段】給光源用的滑鼠拖曳
public class ClickObjectStep5 : MonoBehaviour
{
    public static ClickObjectStep5 script;
    public Camera ViewCamera;
    public LayerMask TargetLayer;
    private GameObject Target;
    private RaycastHit hit;
    public MouseType currentMouseType;

    public int 物件中心點Center = 0;
    public int 物件位置Min = -600;
    public int 物件位置Max = 600;
    //與中心點的距離
    private int dis;

    public float 影子Scale倍率_Min = 1;
    public float 影子Scale倍率_Max = 2;
    public float 影子Position位置偏移_Min = -100;
    public float 影子Position位置偏移_Max = 100;
    // Use this for initialization
    void Start()
    {
        script = this;
        dis = Mathf.Abs(物件中心點Center - 物件位置Min);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouseState();
    }

    /// <summary>
    /// 處理滑鼠狀態
    /// </summary>
    private void ProcessMouseState()
    {
        switch (this.currentMouseType)
        {
            case MouseType.初始播放引導動畫:
                    //播放引導動畫
                    PlayHandBoneAnimation.script.animationType = PlayHandBoneAnimation.AnimationType.太陽引導;
                    PlayHintBoneAnimation.script.animationType = PlayHintBoneAnimation.AnimationType.畫布上方太陽;
                this.currentMouseType = MouseType.無狀態;
                break;

            case MouseType.無狀態:
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.點擊;
                break;

            case MouseType.點擊:

                //通知"下一步"按鈕 可以出現
                NextButtonController.isCheck4 = true;

                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                    this.currentMouseType = MouseType.拖曳中;
                else
                    this.currentMouseType = MouseType.無狀態;
                break;

            case MouseType.拖曳中:


                if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 200, this.TargetLayer))
                {
                    //關閉手指動畫
                    PlayHandBoneAnimation.script.animationType = PlayHandBoneAnimation.AnimationType.空動畫;


                    //物件拖曳範圍 Min~Max
                    this.hit.transform.position = new Vector3(
                       Mathf.Max(Mathf.Min(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition).x, 物件位置Max), 物件位置Min),
                       this.hit.transform.position.y,
                       this.hit.transform.position.z);

                    //物件的影子倍率與位置
                    foreach (GameObject gameObject in State.script.影子)
                    {
                        if (gameObject.activeInHierarchy)
                        {
                            gameObject.transform.localScale = new Vector3(
                                Mathf.Lerp(影子Scale倍率_Min, 影子Scale倍率_Max, Mathf.Abs(this.hit.transform.position.x - 物件中心點Center) / dis),
                                gameObject.transform.localScale.y,
                                gameObject.transform.localScale.z);

                            gameObject.transform.localPosition = new Vector3(
                                    Mathf.Lerp(
                                    影子Position位置偏移_Max,
                                    影子Position位置偏移_Min,
                                     (((this.hit.transform.position.x - 物件中心點Center) / dis) + 1) / 2F
                                    ),
                                gameObject.transform.localPosition.y,
                                gameObject.transform.localPosition.z);
                        }
                    }

                }
                if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    this.currentMouseType = MouseType.無狀態;

                break;

            case MouseType.放開:

                this.currentMouseType = MouseType.無狀態;
                break;
        }


    }

    //定義滑鼠狀態
    public enum MouseType
    {
        無狀態 = 0, 點擊 = 1, 拖曳中 = 2, 放開 = 3, 初始播放引導動畫 = 4
    }
}