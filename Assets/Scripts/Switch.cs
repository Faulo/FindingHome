using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField]
    private float PressedY = 0.08f;

    private ISet<IActor> PressedBy = new HashSet<IActor>();
    private bool IsPressed {
        get {
            return PressedBy.Any();
        }
    }
    private Vector3 OriginalPosition;
    private Vector3 PressedPosition;
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
        PressedPosition = new Vector3(OriginalPosition.x, OriginalPosition.y - PressedY, OriginalPosition.z);
        Mechanisms = FindObjectsOfType<GameObject>()
            .SelectMany(gameObject => gameObject.GetComponents<IMechanism>())
            .Where(mechanism => mechanism.MechanismColor == MechanismColor)
            .ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = IsPressed
            ? PressedPosition
            : OriginalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponents<IActor>()
            .ForAll(actor => PressedBy.Add(actor));
        if (IsPressed) {
            Mechanisms.ForAll(mechanism => mechanism.ActivateMechanism());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponents<IActor>()
            .ForAll(actor => PressedBy.Remove(actor));
        if (!IsPressed) {
            Mechanisms.ForAll(mechanism => mechanism.DeactivateMechanism());
        }
    }
}
