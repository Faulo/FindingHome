using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public static float PlayerDistance {
        get {
            var list = FindObjectsOfType<CubeController>();
            if (list.Count() > 1) {
                return Vector3.Distance(list[0].transform.position, list[1].transform.position);
            } else {
                return float.PositiveInfinity;
            }
        }
    }

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

    [SerializeField]
    private float DashDuration = 1;

    [SerializeField]
    private AudioCollection DashSound;

    private float DashTimer;

    private bool DashReady = true;

    [SerializeField]
    private float InteractRadius = 1;

    private Animator Animator;

    private Vector3 Velocity = Vector3.zero;

    private Rigidbody Rigidbody {
        get {
            return GetComponent<Rigidbody>();
        }
    }

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

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
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

        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, direction, ref Velocity, SmoothTime);//, MaxSpeed);

        Animator.SetBool("isMoving", Rigidbody.velocity.magnitude > 0.01f);

        InteractUpdate();

        DashUpdate();
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("Interact" + Player)) {
            Physics.OverlapSphere(transform.position, InteractRadius)
                .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
                .ForAll(interactable => interactable.Interact(gameObject));
        }
    }

    private void DashUpdate() {
        var dashing = Input.GetAxis("Dash" + Player);
        if (dashing < 0.5) {
            DashReady = true;
        }

        if (IsDashing) {
            DashTimer -= Time.deltaTime;
        } else {
            if (DashReady && dashing > 0.5) {
                Dash();
            }
        }
    }

    private void Dash() {
        DashReady = false;
        Rigidbody.AddForce(transform.forward * DashForce);
        DashTimer = DashDuration;
        FindObjectOfType<AudioManager>().PlayOneShotSound(DashSound, transform.position);
    }
}
