﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    [SerializeField] private AudioCollection _breakSound;

    private Rigidbody[] _wallBody;
    private AudioManager _audio;

    private void Awake()
    {
        _wallBody = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in _wallBody)
        {
            body.isKinematic = true;
        }
    }

    private void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerStay(Collider collider)
    {
        var fox = collider.GetComponents<CubeController>()
            .Where(cube => cube.IsFox && cube.IsDashing)
            .FirstOrDefault();
        if (fox != null)
        {
            foreach (Rigidbody body in _wallBody)
            {
                body.isKinematic = false;
            }
            if (_breakSound)
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
