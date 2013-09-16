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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.P))
           TextureProcess();
    }

    void TextureProcess()
    {
        print(ScreenShot.script.imagePath);
        WWW www = new WWW("file://" + ScreenShot.script.imagePath);
        texture2D = www.texture;
        //改變圖片的格式與可讀取狀態
        ChangeTexture2D_Writable_Format(www.texture);
        //Texture2D 轉成 Image
        image = Texture2Image(www.texture);
    }

    //void PrintImage(object o, PrintPageEventArgs e)
    //{
    //    int x = SystemInformation.WorkingArea.X;
    //    int y = SystemInformation.WorkingArea.Y;
    //    int width = this.Width;
    //    int height = this.Height;

    //    Rectangle bounds = new Rectangle(x, y, width, height);

    //    Bitmap img = new Bitmap(width, height);

    //    //this.DrawToBitmap(img, bounds);
    //    Point p = new Point(100, 100);
    //    e.Graphics.DrawImage(img, p);
    //}

    //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    //{
    //    //列印圖片
    //    e.Graphics.DrawImage(pictureBox1.Image, 160, 20, 250, 100);
    //    //列印文字
    //    e.Graphics.DrawString(textBox1.Text, textBox1.Font, new SolidBrush(Color.Black), new Point(50, 50));
    //}

    /// <summary>
    /// 改變圖片的格式與可讀取狀態
    /// </summary>
    /// <param name="texture2D">圖片</param>
    private void ChangeTexture2D_Writable_Format(Texture2D texture2D)
    {
        string path = AssetDatabase.GetAssetPath(texture2D);
        TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(path);
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
        byte[] bytes = texture2D.EncodeToPNG();
        MemoryStream ms = new MemoryStream(bytes);
        ms.Seek(0, SeekOrigin.Begin);
        Image bmp2 = System.Drawing.Bitmap.FromStream(ms);
        ms.Close();
        ms = null;

        return bmp2;
    }
    #endregion
}
