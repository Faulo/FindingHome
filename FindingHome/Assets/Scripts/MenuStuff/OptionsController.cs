using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
    public Slider environmentalSlider;
    public Slider playerSlider;
	public LevelManager _levelManager;

	private AudioManager _audioManager;

	// Use this for initialization
	void Start () {
		_audioManager = GameObject.FindObjectOfType<AudioManager>();
        _levelManager = FindObjectOfType<LevelManager>();
		//volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		//diffSlider.value = PlayerPrefsManager.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
		_audioManager.Mixer.SetFloat("MasterVol", volumeSlider.value);
        _audioManager.Mixer.SetFloat("EnvironmentVol", environmentalSlider.value);
        _audioManager.Mixer.SetFloat("PlayerVol", playerSlider.value);

    }

	public void SaveAndExit () {
        //PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
        //PlayerPrefsManager.SetDifficulty (diffSlider.value);
        _levelManager.LoadLevel ("0x01 Start");

	}

	public void SetDefaults () {
		volumeSlider.value = -40f;
	}
}
