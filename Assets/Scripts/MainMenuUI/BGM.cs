using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    public static BGM Instance;

    [SerializeField] AudioSource src;       
    [SerializeField] AudioClip defaultClip; 
    [Range(0f, 1f)] public float defaultVolume = 0.7f;

    void Awake()
    {
   
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!src) src = GetComponent<AudioSource>();
        if (!src) src = gameObject.AddComponent<AudioSource>();

        src.loop = true;
        src.playOnAwake = false;
        src.volume = PlayerPrefs.GetFloat("BGM_VOL", defaultVolume);

        if (defaultClip)
        {
            src.clip = defaultClip;
            src.Play();
        }
    }

  
    public void Play(AudioClip clip)
    {
        if (!clip) return;
        if (src.clip == clip && src.isPlaying) return;
        src.clip = clip;
        src.Play();
    }


    public void SetVolume(float v)
    {
        v = Mathf.Clamp01(v);
        src.volume = v;
        PlayerPrefs.SetFloat("BGM_VOL", v);
    }

    public void StopMusic() => src.Stop();
}
