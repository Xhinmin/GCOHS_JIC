using UnityEngine;
using System.Collections;

/// <summary>
/// OHS專案第一步驟：選取圖片
/// </summary>
public class PickObject : MonoBehaviour
{

    public GameObject Target;

    private RaycastHit hit;

    public LayerMask TargetLayer;
    public Camera camera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out hit, 100, this.TargetLayer) == true)
        {
            print(hit.transform.name);
            Target = hit.transform.gameObject;
        }
        else
            Target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1) * 100);
    }
}
