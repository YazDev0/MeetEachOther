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
        // Ã⁄· «·„œÌ— ’Ê  Ê«Õœ ›ﬁÿ ›Ì «·„‘Âœ
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

        // ≈⁄œ«œ „’œ— «·’Ê 
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // œ«·… · ‘€Ì· √Ì ’Ê 
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume * soundEffectsVolume);
        }
    }

    // œ«·… Œ«’… ·’Ê  «·ﬁ›“
    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    // œ«·… Œ«’… ·’Ê  «·Â»Êÿ
    public void PlayLandingSound()
    {
        PlaySound(landingSound);
    }

    // œ«·… Œ«’… ·’Ê  «·ŒÿÊ« 
    public void PlayFootstepSound()
    {
        PlaySound(footstepSound, 0.5f); // ’Ê  √Œ› ··ŒÿÊ« 
    }
}