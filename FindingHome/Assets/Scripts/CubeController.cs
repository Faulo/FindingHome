using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private string Player = "A";

    [SerializeField]
    private float MaxSpeed = 0.1f;

    [SerializeField]
    private float SmoothTime = 0.25f;

    [SerializeField]
    private float TurnSpeed = 1f;

    [SerializeField]
    private float DashForce = 100f;



    private Vector3 Velocity = Vector3.zero;

    private Rigidbody Rigidbody {
        get {
            return GetComponent<Rigidbody>();
        }
    }

    [SerializeField]
    private float DashDuration = 1;
    private float DashTimer;
    public bool IsDashing { get {
            return DashTimer > 0;
        }
    }

    public bool IsFox {
        get {
            return Player == "A";
        }
    }

    public bool IsPingu {
        get {
            return Player == "B";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var horizontal = Input.GetAxis("Horizontal" + Player);
        var vertical = Input.GetAxis("Vertical" + Player);
        var direction = new Vector3(horizontal * MaxSpeed, 0, vertical * MaxSpeed);
        var targetPosition = transform.position + direction;

        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed);
        }
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, direction, ref Velocity, SmoothTime);

        DashUpdate();
        
    }

    private void DashUpdate() {
        if (Input.GetButtonDown("Interact" + Player)) {
            Rigidbody.AddForce(transform.forward * DashForce);
            DashTimer = DashDuration;
        }

        if (IsDashing) {
            DashTimer -= Time.deltaTime;
        }
    }
}
