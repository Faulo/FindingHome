using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IMechanism
{
    bool isActivated = false;

    Vector3 originalPosition;

    public float moveAmount = 2f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition;
        if (isActivated)
            targetPosition = new Vector3(originalPosition.x, originalPosition.y + moveAmount, originalPosition.z);
        else
            targetPosition = originalPosition;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }

    public void ActivateMechanism()
    {
        isActivated = true;
    }

    public void DeactivateMechanism()
    {
        isActivated = false;
    }
}
