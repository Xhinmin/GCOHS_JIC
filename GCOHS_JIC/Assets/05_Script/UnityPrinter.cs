using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;

public class UnityPrinter : MonoBehaviour
{

    public static UnityPrinter script;
    private WWW www;

    // Use this for initialization
    void Start()
    {
        script = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Print();
    }

    void Print()
    {
        //從WWW取得圖片(LOCAL)
        StartCoroutine(GetTextureFromWWW());
        //執行列印
        StartCoroutine(PrintProcess());
    }

    IEnumerator GetTextureFromWWW()
    {
        if (ScreenShot.script.imagePath == "")
        {
            Debug.Log("ScreenShot.script.imagePath is NULL , please check imagePath is set up");
            yield return null;
        }
        //Local 端檔案位置得加上 "file://"
        www = new WWW("file://" + ScreenShot.script.imagePath);
        while (!www.isDone)
            yield return www;
    }

    IEnumerator PrintProcess()
    {
        //實例列印物件
        PrintDocument pd = new PrintDocument();
        //加上事件
        pd.PrintPage += new PrintPageEventHandler(PrintImage);
        //執行列印
        pd.Print();
        yield return null;
    }

    void PrintImage(object o, PrintPageEventArgs e)
    {
        //列印的左上方起始點
        Point startPoint = new Point(0, 0);
        e.Graphics.DrawImage(WWW_Texture2Image(www), startPoint);
    }


    /// <summary>
    /// Unity Texture2D 轉成 Image 資料結構 ， 需要開啟圖片權限 ，Texture2D_Writable_Format 與 using UnityEditor 
    /// 或手動改變圖片權限 Advances -> isReadable = true  textureFormat = TextureImporterFormat.RGBA32
    /// </summary>
    /// <param name="texture2D">圖片</param>
    /// <returns></returns>
    # region texture2image
    public Image Texture2Image(Texture2D texture2D)
    {
        if (texture2D == null)
        {
            return null;
        }
        byte[] bytes = texture2D.EncodeToPNG();
        MemoryStream ms = new MemoryStream(bytes);
        ms.Seek(0, SeekOrigin.Begin);
        Image bmp2 = System.Drawing.Bitmap.FromStream(ms);
        ms.Close();
        ms = null;

        return bmp2;
    }
    #endregion

    /// <summary>
    /// 改騙圖片權限
    /// </summary>
    /// <param name="texture2D">圖片</param>
    private void Texture2D_Writable_Format(Texture2D texture2D)
    {
        string path = AssetDatabase.GetAssetPath(texture2D);
        TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(path);
        ti.isReadable = true;
        ti.textureFormat = TextureImporterFormat.RGBA32;
        AssetDatabase.ImportAsset(path);
    }


    /// <summary>
    /// Unity Texture2D 轉成 Image 資料結構 ， 藉由 WWW 取得Bytes 則不必修改圖片權限
    /// </summary>
    /// <param name="texture2D">圖片</param>
    /// <returns></returns>
    # region texture2image
    public Image WWW_Texture2Image(WWW www)
    {
        if (www == null)
        {
            return null;
        }
        byte[] bytes = www.texture.EncodeToPNG();
        MemoryStream ms = new MemoryStream(bytes);
        ms.Seek(0, SeekOrigin.Begin);
        Image bmp2 = System.Drawing.Bitmap.FromStream(ms);
        ms.Close();
        ms = null;

        return bmp2;
    }
    #endregion
}
