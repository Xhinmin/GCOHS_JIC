using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第一步驟：選取圖片
/// </summary>
public class PickObject : MonoBehaviour
{
    public GameObject Target;

    public Camera camera;

    public LayerMask TargetLayer;

    private RaycastHit hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100, this.TargetLayer) == true)
        {
            print(this.hit.transform.name);
            this.Target = this.hit.transform.gameObject;
        }
        else
            this.Target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1) * 100);
    }
}
