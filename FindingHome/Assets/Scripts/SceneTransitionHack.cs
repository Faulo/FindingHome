using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionHack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneTransition());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("L"))
        {
            FindObjectOfType<LevelManager>().LoadNextLevel();
        }
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(185f);
        FindObjectOfType<LevelManager>().LoadNextLevel();
    }
}
