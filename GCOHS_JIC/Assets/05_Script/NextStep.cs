using UnityEngine;
using System.Collections;

public class NextStep : MonoBehaviour
{
    public Camera ViewCamera;
    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.ViewCamera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1), out this.hit, 100))
        {
            if (hit.transform.gameObject == this.gameObject)
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
                {
                    switch (State.script.stateInfo)
                    {
                        case State.StateInfo.構圖:
                            State.script.stateInfo = State.StateInfo.明暗;
                            break;

                        case State.StateInfo.明暗:
                            State.script.stateInfo = State.StateInfo.設色;
                            break;
                    }
                    
                }
        }
    }
}
