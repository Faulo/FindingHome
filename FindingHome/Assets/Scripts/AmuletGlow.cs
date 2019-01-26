using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletGlow : MonoBehaviour
{
    [SerializeField]
    private float MaxGlow = 1;

    private Light Light {
        get {
            return GetComponent<Light>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Light.range = MaxGlow / CubeController.PlayerDistance;
    }
}
