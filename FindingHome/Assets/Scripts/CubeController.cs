using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeController : MonoBehaviour, IActor {
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
    private PlayerType Player;

    [SerializeField]
    private float MaxSpeed = 2f;

    [SerializeField]
    private float SmoothTime = 0.01f;

    [SerializeField]
    private float TurnSpeed = 0.1f;

    [SerializeField]
    private float DashForce = 1000f;

    [SerializeField]
    private float DashDuration = 0.25f;

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

    public bool IsDashing {
        get {
            return DashTimer > 0;
        }
    }

    public bool IsFox {
        get {
            return Player == PlayerType.Fox;
        }
    }

    public bool IsPingu {
        get {
            return Player == PlayerType.Penguin;
        }
    }

    private string PlayerKey {
        get {
            switch (Player) {
                case PlayerType.Fox:
                    return "A";
                case PlayerType.Penguin:
                    return "B";
            }
            throw new System.Exception("Unknown PlayerType" + Player);
        }
    }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        InteractUpdate();

        DashUpdate();

        var horizontal = Input.GetAxis("Horizontal" + PlayerKey);
        var vertical = Input.GetAxis("Vertical" + PlayerKey);
        var direction = new Vector3(horizontal * MaxSpeed, 0, vertical * MaxSpeed);
        var targetPosition = transform.position + direction;

        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed);
        }

        if (IsDashing) {
            switch (Player) {
                case PlayerType.Fox:
                    direction += transform.forward * DashForce;
                    break;
                case PlayerType.Penguin:
                    direction += transform.forward * DashForce;
                    break;
            }
        }

        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, direction, ref Velocity, SmoothTime);

        Animator.SetBool("isMoving", Rigidbody.velocity.magnitude > 0.01f);
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("Interact" + PlayerKey)) {
            Physics.OverlapSphere(transform.position, InteractRadius)
                .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
                .ForAll(interactable => interactable.Interact(gameObject));
        }
    }

    private void DashUpdate() {
        var dashing = Input.GetAxis("Dash" + PlayerKey);
        if (dashing < 0.5) {
            DashReady = true;
        }

        if (IsDashing) {
            DashTimer -= Time.deltaTime;
        } else {
            if (DashReady && dashing > 0.5) {
                DashReady = false;
                DashTimer = DashDuration;
                FindObjectOfType<AudioManager>().PlayOneShotSound(DashSound, transform.position);
            }
        }
    }
}
