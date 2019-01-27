using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{

    [SerializeField] Transform _pinguinPoint;
    [SerializeField] private Transform _foxPoint;

    private GameObject _fox;
    private GameObject _pinguin;

    private Animator _animator;
    private Camera _camera;
    private Collider _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _camera = GetComponentInChildren<Camera>();
        _camera.enabled = false;

        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fox")
            _fox = other.transform.parent.gameObject;

        if (other.tag == "Pingu")
            _pinguin = other.transform.parent.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_fox && _pinguin)
        {
            _fox.transform.parent = gameObject.transform;
            _pinguin.transform.parent = gameObject.transform;

            _camera.enabled = true;
            _fox.GetComponentInChildren<Camera>().enabled = false;
            _pinguin.GetComponentInChildren<Camera>().enabled = false;

            _fox = _fox.GetComponentInChildren<CubeController>().gameObject;
            _pinguin = _pinguin.GetComponentInChildren<CubeController>().gameObject;

            _fox.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _pinguin.GetComponent<Rigidbody>().velocity = Vector3.zero;

            _fox.GetComponent<CubeController>().transform.position = _foxPoint.transform.position;
            _pinguin.GetComponent<CubeController>().transform.position = _pinguinPoint.transform.position;

            _fox.GetComponentInChildren<Animator>().SetBool("isMoving", false);
            _pinguin.GetComponentInChildren<Animator>().SetBool("isMoving", false);

            _fox.GetComponent<CubeController>().enabled = false;
            _pinguin.GetComponent<CubeController>().enabled = false;

            _fox.transform.eulerAngles = new Vector3(0, 90, 0);
            _pinguin.transform.eulerAngles = new Vector3(0, -90, 0);

            _animator.SetBool("startWinScene", true);

            _collider.enabled = false;

            Invoke("StopAnimator", 2);

            enabled = false;
        }
    }

    // Update is called once per frame
    void StopAnimator()
    {
        _fox.GetComponentInChildren<Animator>().enabled = false;
        _pinguin.GetComponentInChildren<Animator>().enabled = false;
    }
}
