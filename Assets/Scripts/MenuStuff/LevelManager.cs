using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;

    public float autoLoadNextLevelAfter;
    public static LevelManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        { Destroy(gameObject); }
        else
        {
            // This object must live for the entire application
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
		if(autoLoadNextLevelAfter <= 0) {
			Debug.Log("Level auto load disabled, use a positive number in seconds");
		} else {
			Invoke("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            Application.Quit();
        }
        if (Input.GetButtonDown("Submit")) {
            LoadNextLevel();
        }
    }

	public void LoadLevel(string name){
		Debug.Log ("Level load requested for: " + name);
        SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Debug.Log("I Want to Quit!");
        Application.Quit();
	}

	public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
