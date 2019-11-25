using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private AudioCollection _breakSound;
    private AudioManager _audio;

    private void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _audio.PlayOneShotSound(
                        _breakSound.AudioGroup,
                        _breakSound.audioClip,
                        transform.position,
                        _breakSound.Volume,
                        _breakSound.SpatialBlend,
                        _breakSound.Priority);
            
            this.enabled = false;
        }
    }
}
