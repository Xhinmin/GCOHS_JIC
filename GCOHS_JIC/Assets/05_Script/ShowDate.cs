using UnityEngine;
using System.Collections;

public class ShowDate : MonoBehaviour
{
    public Rect TextRect;
    public GUIStyle style;
    private string DateString;
    private float originFontSize;
    private System.DateTime time = System.DateTime.Now;

    // Use this for initialization
    void Start()
    {
        this.originFontSize = this.style.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        this.DateString = time.Year.ToString() + @"/" + time.Month.ToString() + @"/" + time.Day;
        this.style.fontSize = (int)(this.originFontSize * (float)(Screen.width / 1280.0f));
    }

    void OnGUI()
    {
        GUI.Label(new Rect(this.TextRect.x * Screen.width, this.TextRect.y * Screen.height, this.TextRect.width * Screen.width, this.TextRect.height * Screen.height), this.DateString, this.style);
    }
}
