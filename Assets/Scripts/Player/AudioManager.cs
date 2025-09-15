using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip footstepSound;

    [Range(0f, 1f)]
    public float soundEffectsVolume = 0.7f;

    private AudioSource audioSource;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

  
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume * soundEffectsVolume);
        }
    }


    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

  
    public void PlayLandingSound()
    {
        PlaySound(landingSound);
    }

    public void PlayFootstepSound()
    {
        PlaySound(footstepSound, 0.5f); 
    }
}