using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private float SmoothTime = 1f;

    private Vector3 Velocity = Vector3.zero;

    private Vector3 Offset;

    enum Mode {follow, blend }

    Mode mode = Mode.follow;

    public Vector3 SnapPoint;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInSnapZone())
        {
            Debug.Log(gameObject.name + " is in SnapZone");
            mode = Mode.blend;
        } else
        {
            Debug.Log(gameObject.name + " is not in SnapZone");
            mode = Mode.follow;
        }

        switch(mode)
        {
            case Mode.follow:
                Vector3 targetPosition = Offset + Target.position;

                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, SmoothTime+.1f);
                GetComponent<Camera>().orthographic = false;
                break;

            case Mode.blend:
                print("SnapPoint.x: " + SnapPoint.x);

                transform.position = Vector3.SmoothDamp(transform.position, SnapPoint, ref Velocity, SmoothTime+1.1f);
                GetComponent<Camera>().orthographic = true;

                //transform.position = SnapPoint;

                print("camPos.x: " + transform.position.x);
                break;
            default:
                break;
        }
    }

    bool IsInSnapZone()
    {
        Vector2 lowerLeftBound = new Vector2(-3.5f, -3.5f);
        Vector2 upperRightBound = new Vector2(3.5f, 3.5f);

        Vector2 camPosition = new Vector2(Target.position.x, Target.position.z);

        if (camPosition.x <= upperRightBound.x && camPosition.x >= lowerLeftBound.x && camPosition.y <= upperRightBound.y && camPosition.y >= lowerLeftBound.y)
        {
            return true;
        }

        return false;
    }
}
