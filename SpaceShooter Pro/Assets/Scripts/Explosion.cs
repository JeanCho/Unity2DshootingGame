using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudioSource;
    [SerializeField]
    private AudioClip _explosionAudioClip;
    void Start()
    {
        _explosionAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource.clip = _explosionAudioClip;
        if(_explosionAudioSource == null)
        {
            Debug.LogError("Explosion Audio Source is null");
        }
        _explosionAudioSource.Play();
        Destroy(this.gameObject, 2.5f);
    }

    
}
