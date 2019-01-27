using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private float SmoothTime = 1f;

    Transform Room;

    private Vector3 Velocity = Vector3.zero;

    private Vector3 Offset;

    [SerializeField]
    bool blend = true;
    bool isFoxCam;

    enum Mode {follow, blend }

    Mode mode = Mode.follow;
    [SerializeField]
    Vector3 SnapPoint;

    // Start is called before the first frame update
    void Start()
    {
        Room = GameObject.Find("RoomPosition").transform;
        if (Room == null) blend = false;

        if (transform.parent.name == "Player.Fox") isFoxCam = true;
        else isFoxCam = false;

        Offset = transform.localPosition;

        SnapPoint = new Vector3(Room.position.x + 3, 5, 0);
        if (isFoxCam) SnapPoint.x *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInSnapZone() && blend)
        {
            mode = Mode.blend;
        } else
        {
            mode = Mode.follow;
        }

        switch(mode)
        {
            case Mode.follow:
                Vector3 targetPosition = Offset + Target.position;

                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, SmoothTime);
                GetComponent<Camera>().orthographic = false;
                break;

            case Mode.blend:
                transform.position = Vector3.SmoothDamp(transform.position, SnapPoint, ref Velocity, SmoothTime+1.1f);
                GetComponent<Camera>().orthographic = true;
                break;
            default:
                break;
        }
    }

    bool IsInSnapZone()
    {
        Vector2 lowerLeftBound = new Vector2(Room.position.x-3.5f, Room.position.z - 3.5f);
        Vector2 upperRightBound = new Vector2(Room.position.x + 3.5f, Room.position.z + 3.5f);

        Vector2 camPosition = new Vector2(Target.position.x, Target.position.z);

        if (camPosition.x <= upperRightBound.x && camPosition.x >= lowerLeftBound.x && camPosition.y <= upperRightBound.y && camPosition.y >= lowerLeftBound.y)
        {
            return true;
        }

        return false;
    }
}
