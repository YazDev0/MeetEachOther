using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    public static BGM Instance;

    [SerializeField] AudioSource src;        // «”Õ» AudioSource Â‰« (√Ê Ì·ﬁÿÂ  ·ﬁ«∆Ì«)
    [SerializeField] AudioClip defaultClip;  // («Œ Ì«—Ì) „ﬁÿ⁄ «·»œ«Ì…
    [Range(0f, 1f)] public float defaultVolume = 0.7f;

    void Awake()
    {
        // «„‰⁄ «· ﬂ—«— »Ì‰ «·”Ì¯‰« 
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

    // «” œ⁄ˆÂ« · €ÌÌ— «·„ﬁÿ⁄ √À‰«¡ «··⁄» («Œ Ì«—Ì)
    public void Play(AudioClip clip)
    {
        if (!clip) return;
        if (src.clip == clip && src.isPlaying) return;
        src.clip = clip;
        src.Play();
    }

    //  Õﬂ„ »«·’Ê  („À·« „‰ Slider)
    public void SetVolume(float v)
    {
        v = Mathf.Clamp01(v);
        src.volume = v;
        PlayerPrefs.SetFloat("BGM_VOL", v);
    }

    // √Êﬁ› «·„Ê”ÌﬁÏ (·Ê  »€Ï  Êﬁ›Â« ›Ì ”Ì¯‰ „⁄Ì‰)
    public void StopMusic() => src.Stop();
}
