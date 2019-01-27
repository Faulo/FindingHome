using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneHack : MonoBehaviour
{
    public GameObject Final;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowCredits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(8f);
        Final.SetActive(false);
    }
}
