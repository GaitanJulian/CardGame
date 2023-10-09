using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip clickSound;
    public AudioClip musicBackground;

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the AudioManager GameObject from being destroyed on scene changes.
        }
        else
        {
            // If another AudioManager exists, destroy this one.
            Destroy(gameObject);
        }

        musicSource.clip = musicBackground;
        musicSource.Play();
    }

    public void PlaySFXSound(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
