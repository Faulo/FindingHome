using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable {
    [SerializeField]
    private AudioCollection LightSound;

    private bool IsLit = false;

    public void Interact(GameObject actor) {
        IsLit = !IsLit;
        if (IsLit) {
            FindObjectOfType<AudioManager>().PlayOneShotSound(LightSound, transform.position);
        }
    }

    private GameObject Sparks {
        get {
            return transform.Find("Sparks").gameObject;
        }
    }
    private GameObject Ember {
        get {
            return transform.Find("Ember").gameObject;
        }
    }

    private ParticleSystem.EmissionModule SparksEmission {
        get {
            return Sparks.GetComponent<ParticleSystem>().emission;
        }
    }
    private Light TorchLight {
        get {
            return GetComponentInChildren<Light>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var emission = SparksEmission;

        emission.enabled = IsLit;
        TorchLight.enabled = IsLit;
    }
}
