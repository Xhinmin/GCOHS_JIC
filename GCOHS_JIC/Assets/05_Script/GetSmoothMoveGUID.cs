using UnityEngine;
using System.Collections;

public class GetSmoothMoveGUID : MonoBehaviour
{
    public GUID_From guid_form;
    // Use this for initialization
    void Start()
    {
        string guid = this.GetComponent<SmoothMoves.Sprite>().textureGUID;

        switch (guid_form)
        {
            case GUID_From.馬1:
                GameManager.script.馬1_GUID = guid;
                break;
            case GUID_From.馬2:
                GameManager.script.馬2_GUID = guid;
                break;
            case GUID_From.馬3:
                GameManager.script.馬3_GUID = guid;
                break;
            case GUID_From.樹1:
                GameManager.script.樹1_GUID = guid;
                break;
            case GUID_From.樹2:
                GameManager.script.樹2_GUID = guid;
                break;
            case GUID_From.土坡1:
                GameManager.script.土坡1_GUID = guid;
                break;
            case GUID_From.土坡2:
                GameManager.script.土坡2_GUID = guid;
                break;
            case GUID_From.土坡3:
                GameManager.script.土坡3_GUID = guid;
                break;

            case GUID_From.馬1顏色1:
                GameManager.script.馬1顏色_GUID[0] = guid;
                break;
            case GUID_From.馬1顏色2:
                GameManager.script.馬1顏色_GUID[1] = guid;
                break;
            case GUID_From.馬1顏色3:
                GameManager.script.馬1顏色_GUID[2] = guid;
                break;

            case GUID_From.馬2顏色1:
                GameManager.script.馬2顏色_GUID[0] = guid;
                break;
            case GUID_From.馬2顏色2:
                GameManager.script.馬2顏色_GUID[1] = guid;
                break;
            case GUID_From.馬2顏色3:
                GameManager.script.馬2顏色_GUID[2] = guid;
                break;
            case GUID_From.馬3顏色1:
                GameManager.script.馬3顏色_GUID[0] = guid;
                break;
            case GUID_From.馬3顏色2:
                GameManager.script.馬3顏色_GUID[1] = guid;
                break;
            case GUID_From.馬3顏色3:
                GameManager.script.馬3顏色_GUID[2] = guid;
                break;


            case GUID_From.樹1顏色1:
                GameManager.script.樹1顏色_GUID[0] = guid;
                break;
            case GUID_From.樹1顏色2:
                GameManager.script.樹1顏色_GUID[1] = guid;
                break;
            case GUID_From.樹1顏色3:
                GameManager.script.樹1顏色_GUID[2] = guid;
                break;

            case GUID_From.樹2顏色1:
                GameManager.script.樹2顏色_GUID[0] = guid;
                break;
            case GUID_From.樹2顏色2:
                GameManager.script.樹2顏色_GUID[1] = guid;
                break;
            case GUID_From.樹2顏色3:
                GameManager.script.樹2顏色_GUID[2] = guid;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum GUID_From
    {
        馬1 = 0, 馬2 = 1, 馬3 = 2, 樹1 = 3, 樹2 = 4, 土坡1 = 5, 土坡2 = 6, 土坡3 = 7,

        馬1顏色1 = 21, 馬1顏色2 = 22, 馬1顏色3 = 23,
        馬2顏色1 = 24, 馬2顏色2 = 25, 馬2顏色3 = 26,
        馬3顏色1 = 27, 馬3顏色2 = 28, 馬3顏色3 = 29,
        樹1顏色1 = 30, 樹1顏色2 = 31, 樹1顏色3 = 32,
        樹2顏色1 = 33, 樹2顏色2 = 34, 樹2顏色3 = 35
    }
}
