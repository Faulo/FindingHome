using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 6f;
    public float yDistanceToTarget = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yDistanceToTarget, target.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, smoothTime * Time.deltaTime);
    }
}
