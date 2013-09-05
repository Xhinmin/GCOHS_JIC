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
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum GUID_From
    {
        馬1 = 0, 馬2 = 1, 馬3 = 2, 樹1 = 3, 樹2 = 4, 土坡1 = 5, 土坡2 = 6, 土坡3 = 7
    }
}
