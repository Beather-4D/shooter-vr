using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAmbient : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] ambientThemes;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ambientThemes[UnityEngine.Random.Range(0, ambientThemes.Length)];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
