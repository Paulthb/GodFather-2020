using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGrandma : MonoBehaviour
{
    public AudioClip happy;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = happy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cat") && !_audio.isPlaying)
        {
            _audio.Play();
            GetComponent<Animator>().SetTrigger("Happy");
        }
    }
}
