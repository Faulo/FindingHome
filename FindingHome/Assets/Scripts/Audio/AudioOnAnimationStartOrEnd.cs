using UnityEngine;

/// <summary>
/// Plays a Audio that shall be on enter of a animation state
/// </summary>
public class AudioOnAnimationStartOrEnd : StateMachineBehaviour
{
    // Private
    private GameObject _soundEmitter;

    // Exposed Fields
    [Tooltip("Play Sound on Animation Start if not selected.")]
    [SerializeField] bool _playOnAnimationEnd;
    [Header("Audio Files")]
    [SerializeField] private AudioCollection _audioCollection;
    [SerializeField] private int _bank;
    [Header("Sound Emitter")]
    [SerializeField] private GameObject _soundEmitterPrefab;
    [SerializeField] private float _decayRate = 3.5f;
    [SerializeField] private float _soundRadius = 10f;
    [Tooltip("Will be set automatically to true if 'Decrease Radius Multiplier' is not 0.")]
    [SerializeField] private bool _keepRunning;
    [SerializeField] [Range(0.0f, 1.0f)] private float _decreaseRadiusMultiplier;
    [Tooltip("Only Works for Sounds that Play on Animation Start.")]
    [SerializeField] private bool _killOnAnimationExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnAnimationEnd == false)
            PlaySound(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnAnimationEnd == false)
            if (_killOnAnimationExit)
                Destroy(_soundEmitter);

        if (_playOnAnimationEnd == true)
            PlaySound(animator);
    }

    //---------------------------------------------------------------------
    private void PlaySound(Animator animator)
    {
        if (AudioManager.Instance != null || _audioCollection != null)
        {

            AudioManager.Instance.PlayOneShotSound(_audioCollection.AudioGroup,
                                                    _audioCollection[_bank],
                                                    animator.transform.position,
                                                    _audioCollection.Volume,
                                                    _audioCollection.SpatialBlend,
                                                    _audioCollection.Priority);
        }
    }
}
