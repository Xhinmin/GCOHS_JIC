using UnityEngine;
using System.Collections;

public class ShowDate : MonoBehaviour
{
    private System.DateTime time = System.DateTime.Now;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(time.Year.ToString() + @"/" + time.Month.ToString() + @"/" + time.Day);
    }
}
