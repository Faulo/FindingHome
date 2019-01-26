using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool isPressed = false;
    Vector3 originalPosition;
    float moveAmount = 0.08f;

    public GameObject mechanism;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition;
        if (isPressed)
            targetPosition = new Vector3(originalPosition.x, originalPosition.y - moveAmount, originalPosition.z);
        else
            targetPosition = originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        isPressed = true;
        mechanism.GetComponents<IMechanism>()
            .ForAll(mechanism => mechanism.ActivateMechanism());
    }

    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
        mechanism.GetComponents<IMechanism>()
            .ForAll(mechanism => mechanism.DeactivateMechanism());
    }
}
