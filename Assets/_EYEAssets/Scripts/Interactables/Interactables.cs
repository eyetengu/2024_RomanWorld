using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Interactables : MonoBehaviour
{
    //public enum _interactableType { Openable, Collectible, Explodable, Climbable };
    //_interactableType _type;

    //private string _name;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private GameObject _particleEffect;
    private AudioSource _audioSource;
    [SerializeField] private float _particleResetDelay;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void UpdateUIManagerMessage(string message)
    {

    }

    //AUDIO / VISUAL EFFECTS
    public virtual void PlayAudioClip()
    {
        _audioSource.PlayOneShot(_audioClip);
    }

    public virtual void RunParticleEffect()
    {
        if (_particleEffect != null)
        {
            _particleEffect.SetActive(true);
            StartCoroutine(ResetParticleEffect());
        }
    }

    //TRIGGER FUNCTIONS
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            RunParticleEffect();
        }
    }

    //Coroutines
    IEnumerator ResetParticleEffect()
    {
        yield return new WaitForSeconds(_particleResetDelay);
        _particleEffect.SetActive(false);
    }
}
