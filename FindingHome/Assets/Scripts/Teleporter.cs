using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    private AudioCollection TeleportSound;

    private static ISet<CubeController> Subjects = new HashSet<CubeController>();
    private static bool SwitchHappened = false;
    private static Coroutine SwitchRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (SwitchRoutine == null) {
            if (Subjects.Count > 1) {
                var a = Subjects.RandomElement();
                Subjects.Remove(a);
                var b = Subjects.RandomElement();
                Subjects.Remove(b);

                SwitchRoutine = StartCoroutine(Switcheroo(a, b));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SwitchRoutine == null) {
            other.GetComponents<CubeController>()
                .ForAll(cube => Subjects.Add(cube));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SwitchRoutine == null) {
            other.GetComponents<CubeController>()
                .ForAll(cube => Subjects.Remove(cube));
        }
    }

    private IEnumerator Switcheroo(CubeController a, CubeController b) {
        yield return null;

        FindObjectOfType<AudioManager>().PlayOneShotSound(TeleportSound, a.transform.position);
        FindObjectOfType<AudioManager>().PlayOneShotSound(TeleportSound, b.transform.position);

        var backup = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = backup;

        yield return new WaitForFixedUpdate();

        SwitchRoutine = null;
    }
}
