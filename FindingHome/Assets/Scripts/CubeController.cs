using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private string Player = "A";

    [SerializeField]
    private float MaxSpeed = 6;

    private Rigidbody Rigidbody {
        get {
            return GetComponent<Rigidbody>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact" + Player)) {
            Rigidbody.AddForce(new Vector3(0, 300, 0));
        } else {
            var horizontal = Input.GetAxis("Horizontal" + Player);
            var vertical = -1 * Input.GetAxis("Vertical" + Player);

            Rigidbody.velocity = new Vector3(horizontal * MaxSpeed, Rigidbody.velocity.y, vertical * MaxSpeed);
        }
    }
}
