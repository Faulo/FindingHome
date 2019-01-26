using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleDoor : MonoBehaviour, IMechanism {
    public Color MechanismColor => GetComponentsInChildren<MeshRenderer>()
        .Select(renderer => renderer.material)
        .Select(material => material.color)
        .First();

    public void ActivateMechanism() {
        GetComponent<Animator>().SetBool("DoorOpen", true);
    }

    public void DeactivateMechanism() {
        GetComponent<Animator>().SetBool("DoorOpen", false);
    }
}
