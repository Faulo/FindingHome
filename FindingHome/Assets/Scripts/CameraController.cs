using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private float SmoothTime = 0;

    private Vector3 Velocity = Vector3.zero;

    private Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Offset + Target.position;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, SmoothTime);
    }
}
