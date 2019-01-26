using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    private Rigidbody[] _wallBody;

    private void Awake()
    {
        _wallBody = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in _wallBody)
        {
            body.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fox")
        {
            foreach (Rigidbody body in _wallBody)
            {
                body.isKinematic = false;
            }
        }
    }

}
