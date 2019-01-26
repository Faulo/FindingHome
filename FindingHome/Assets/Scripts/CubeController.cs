using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private float InteractRadius = 1;


    private bool _isMoving = false;

    private Animator _animator;
    private Vector3 Velocity = Vector3.zero;

    private Rigidbody Rigidbody {
        get {
            return GetComponent<Rigidbody>();
        }
    }

    [SerializeField]
    private float DashDuration = 1;
    private float DashTimer;

    [SerializeField]
    private AudioCollection DashSound;

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
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        var horizontal = Input.GetAxis("Horizontal" + Player);
        var vertical = Input.GetAxis("Vertical" + Player);
        var direction = new Vector3(horizontal * MaxSpeed, 0, vertical * MaxSpeed);
        var targetPosition = transform.position + direction;

        print(direction);
        if (Input.GetButton("Horizontal" + Player) || Input.GetButton("Vertical" + Player)) _isMoving = true;
        else _isMoving = false;

        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed);
        }

        if (_isMoving)
            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, direction, ref Velocity, SmoothTime * Time.deltaTime);//, MaxSpeed);
        

        InteractUpdate();

        DashUpdate();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("isMoving", _isMoving);
    }

    private void InteractUpdate() {
        if (Input.GetButtonDown("Interact" + Player)) {
            Physics.OverlapSphere(transform.position, InteractRadius)
                .SelectMany(collider => collider.gameObject.GetComponents<IInteractable>())
                .ForAll(interactable => interactable.Interact(gameObject));
        }
    }

    private void DashUpdate() {
        if (IsDashing) {
            DashTimer -= Time.deltaTime;
        } else {
            var dashing = Input.GetAxis("Dash" + Player);
            if (dashing > 0.5) {
                Rigidbody.AddForce(transform.forward * DashForce);
                DashTimer = DashDuration;
                FindObjectOfType<AudioManager>().PlayOneShotSound(DashSound, transform.position);
            }
        }
    }
}
