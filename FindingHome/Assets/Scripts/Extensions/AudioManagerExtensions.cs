﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManagerExtensions 
{
    public static void PlayOneShotSound(this AudioManager manager, AudioCollection collection, Vector3 position) {
        manager.PlayOneShotSound(
            collection.AudioGroup,
            collection.audioClip,
            position,
            collection.Volume,
            collection.SpatialBlend,
            collection.Priority
        );
    }
}
