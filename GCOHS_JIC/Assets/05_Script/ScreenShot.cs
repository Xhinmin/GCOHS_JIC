using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    public static ScreenShot script;

    public string imagePath;

    // Use this for initialization
    void Awake()
    {
        script = this;
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            this.imagePath = Application.persistentDataPath;
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
            this.imagePath = Application.dataPath;
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            this.imagePath = Application.dataPath;
            this.imagePath = imagePath.Replace("/Assets", null);
        }

        this.imagePath = Path.Combine(this.imagePath, "PictureResult");
        if (!Directory.Exists(this.imagePath))
            Directory.CreateDirectory(this.imagePath);
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

        System.DateTime currentTime = System.DateTime.Now;
        string fileName = currentTime.Year + "_" + currentTime.Month + "_" + currentTime.Day + "_" + currentTime.Hour + "_" + currentTime.Minute + "_" + currentTime.Second + "_" + currentTime.Millisecond + ".png";
        this.imagePath = Path.Combine(this.imagePath, fileName);

        Application.CaptureScreenshot(this.imagePath);

        print(this.imagePath);

        yield return new WWW(this.imagePath);

        SendEmail.script.UIEnable = true;
        GameManager.script.CurrentDrawStage = GameManager.DrawStage.寄信;
    }
}
