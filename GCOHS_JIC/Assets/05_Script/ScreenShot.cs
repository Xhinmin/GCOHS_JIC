using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{
    public static ScreenShot script;

    public string imagePath;

    // Use this for initialization
    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.RunScreenCapture();
        }
    }

    public void RunScreenCapture()
    {
        StartCoroutine(SC());
    }

    IEnumerator SC()
    {
        yield return new WaitForSeconds(0.1f);

        Application.CaptureScreenshot("ScreenCapture.png");
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            this.imagePath = Application.persistentDataPath;
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
            this.imagePath = Application.dataPath;
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            this.imagePath = Application.dataPath;
            this.imagePath = imagePath.Replace("/Assets", null);
        }
        this.imagePath = this.imagePath + "/ScreenCapture.png";
        print(this.imagePath);

        yield return new WWW(this.imagePath);

        SendEmail.script.UIEnable = true;
        GameManager.script.CurrentDrawStage = GameManager.DrawStage.寄信;
    }
}
