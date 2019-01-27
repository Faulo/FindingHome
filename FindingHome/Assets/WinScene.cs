using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{

    [SerializeField] Transform _pinguinPoint;
    [SerializeField] private Transform _foxPoint;
    [SerializeField] Camera _endCamera;
    [SerializeField] Camera _foxCamera;
    [SerializeField] Camera _pinguCamera;

    private GameObject _fox;
    private GameObject _pinguin;

    private Animator _animator;
    private Collider _collider;

    private bool atBottom = false;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _endCamera.enabled = false;

        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fox")
        {
            _fox = other.transform.parent.gameObject;
            _fox.transform.parent = gameObject.transform;
        }

        if (other.tag == "Pingu")
        {
            _pinguin = other.transform.parent.gameObject;
            _pinguin.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_fox && _pinguin)
        {
            _endCamera.enabled = true;
            _foxCamera.enabled = false;
            _pinguCamera.enabled = false;

            _fox = _fox.GetComponentInChildren<CubeController>().gameObject;
            _pinguin = _pinguin.GetComponentInChildren<CubeController>().gameObject;
            _fox.transform.parent = transform;
            _pinguin.transform.parent = transform;

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

            StartCoroutine(EndScene());

            _collider.enabled = false;
        }
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2);

        _fox.GetComponentInChildren<Animator>().enabled = false;
        _pinguin.GetComponentInChildren<Animator>().enabled = false;
        _animator.enabled = true;

        _animator.SetBool("startWinScene", true);

        yield return new WaitForSeconds(1);

        _animator.SetBool("startWinScene", false);
        _animator.enabled = false;

        yield return new WaitForSeconds(1);

        _animator.enabled = true;
        _animator.SetBool("startWinScene", true);
    }
}
