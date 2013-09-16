using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;

public class UnityPrinter : MonoBehaviour
{

    public PrintPageEventArgs printPageEventArgs;
    public Texture2D texture2D;
    public Image image;
    private string path;
    private WWW www;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TextureProcess();
        if (Input.GetKeyDown(KeyCode.O))
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = false;
            pd.PrintPage += new PrintPageEventHandler(PrintImage);
           // pd.Print();
            pd.Print();
        }

    }

    void TextureProcess()
    {
        StartCoroutine(GetTextureFromWWW());
    }

    IEnumerator GetTextureFromWWW()
    {
        www = new WWW("file://" + ScreenShot.script.imagePath);

        while (!www.isDone)
            yield return www;

        //texture2D = www.bytes;
        //改變圖片的格式與可讀取狀態
        //ChangeTexture2D_Writable_Format(www.texture);
        //Texture2D 轉成 Image
        image = Texture2Image(www.texture);
    }

    void PrintImage(object o, PrintPageEventArgs e)
    {
        int x = 0;
        int y = 0;
        int width = www.texture.width;
        int height = www.texture.height;

        Rectangle bounds = new Rectangle(x, y, width, height);

        Bitmap img = new Bitmap(width, height);

        //this.DrawToBitmap(img, bounds);
        Point p = new Point(0, 0);
        e.Graphics.DrawImage(Texture2Image(www.texture),p);
    }



    /// <summary>
    /// 改變圖片的格式與可讀取狀態
    /// </summary>
    /// <param name="texture2D">圖片</param>
    private void ChangeTexture2D_Writable_Format(Texture2D texture2D)
    {
        string path = AssetDatabase.GetAssetPath(texture2D);
        TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(ScreenShot.script.imagePath);
        ti.isReadable = true;
        ti.textureFormat = TextureImporterFormat.RGBA32;
        AssetDatabase.ImportAsset(path);
    }


    /// <summary>
    /// Unity Texture2D 轉成 Image 資料結構
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
