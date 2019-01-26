using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool isPressed = false;
    Vector3 OriginalPosition;
    float moveAmount = 0.08f;

    private IEnumerable<IMechanism> Mechanisms;

    private Color MechanismColor {
        get {
            return GetComponent<MeshRenderer>().material.color;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        Mechanisms = FindObjectsOfType<GameObject>()
            .SelectMany(gameObject => gameObject.GetComponents<IMechanism>())
            .Where(mechanism => mechanism.MechanismColor == MechanismColor);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition;
        if (isPressed)
            targetPosition = new Vector3(OriginalPosition.x, OriginalPosition.y - moveAmount, OriginalPosition.z);
        else
            targetPosition = OriginalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        isPressed = true;
        Mechanisms.ForAll(mechanism => mechanism.ActivateMechanism());
    }

    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
        Mechanisms.ForAll(mechanism => mechanism.DeactivateMechanism());
    }
}
