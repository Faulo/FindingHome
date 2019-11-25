using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManagerExtensions 
{
    public static void PlayOneShotSound(this AudioManager manager, AudioCollection collection, Vector3 position) {
        if (collection == null || manager == null) {
            return;
        }
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
