using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource CardsSFX;
    [SerializeField] private AudioSource CorrectSFX;

    [Header("Audio Clip")]
    public AudioClip clickSound;
    public AudioClip musicBackground;

    [Header("Game state Sounds")]
    public AudioClip[] errorSounds;

    public AudioClip[] confimationSounds;

    [Header("Card Sounds")]
    public AudioClip[] cardSounds;

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

    // Play a random card sound.
    public void PlayRandomCardSound()
    {
        int randomIndex = Random.Range(0, cardSounds.Length);
        CardsSFX.PlayOneShot(cardSounds[randomIndex]);
    }

    // Play a random error sound.
    public void PlayRandomErrorSound()
    {
        int randomIndex = Random.Range(0, errorSounds.Length);
        CorrectSFX.PlayOneShot(errorSounds[randomIndex]);
    }

    // Play a random correct sound.
    public void PlayRandomCorrectSound()
    {
        int randomIndex = Random.Range(0, confimationSounds.Length);
        CorrectSFX.PlayOneShot(confimationSounds[randomIndex]);
    }
}
