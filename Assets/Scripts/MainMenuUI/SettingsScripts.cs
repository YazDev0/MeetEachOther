using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScripts : MonoBehaviour
{
    public Slider masterVolume;        // 0..1
    public GameObject panel;

    [Header("AudioMixer")]
    public AudioMixer mixer;           // ÃÓÍÈå ãä ÇáãÔÑæÚ
    public string exposedParam = "MasterVolume"; // ÇÓã ÇáÈÇÑÇãíÊÑ ÇáãßÔæİ

    const string VOL_KEY = "vol";

    // ŞÇÆãÉ ÈÌãíÚ ãÕÇÏÑ ÇáÕæÊ İí ÇááÚÈÉ
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    void Awake()
    {
        if (masterVolume) masterVolume.onValueChanged.AddListener(OnVolumeChanged);

        // ÇáÈÍË Úä ÌãíÚ ãÕÇÏÑ ÇáÕæÊ İí ÇáãÔåÏ
        FindAllAudioSources();
    }

    void Start()
    {
        float vol = PlayerPrefs.GetFloat(VOL_KEY, 1f);

        if (masterVolume) masterVolume.value = vol;

        OnVolumeChanged(vol);
    }

    void OnDestroy()
    {
        if (masterVolume) masterVolume.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float v)
    {
        // ÇáÊÍßã İí Audio Mixer
        float dB = (v <= 0.0001f) ? -80f : Mathf.Log10(v) * 20f;
        if (mixer) mixer.SetFloat(exposedParam, dB);

        // ÇáÊÍßã ÇáãÈÇÔÑ İí ÌãíÚ ãÕÇÏÑ ÇáÕæÊ (ßÎØæÉ ÇÍÊíÇØíÉ)
        SetAllAudioSourcesVolume(v);

        PlayerPrefs.SetFloat(VOL_KEY, v);
        PlayerPrefs.Save();
    }

    // ÇáÈÍË Úä ÌãíÚ ãÕÇÏÑ ÇáÕæÊ İí ÇáãÔåÏ
    void FindAllAudioSources()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        allAudioSources.Clear();
        allAudioSources.AddRange(sources);
    }

    // ÊÚííä volume áÌãíÚ ãÕÇÏÑ ÇáÕæÊ
    void SetAllAudioSourcesVolume(float volume)
    {
        foreach (AudioSource source in allAudioSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }

    // ÊÍÏíË ŞÇÆãÉ ãÕÇÏÑ ÇáÕæÊ ÚäÏ İÊÍ ÇáÅÚÏÇÏÇÊ
    public void OpenSettings()
    {
        if (panel) panel.SetActive(true);
        FindAllAudioSources(); // ÊÍÏíË ÇáŞÇÆãÉ ÚäÏ İÊÍ ÇáÅÚÏÇÏÇÊ
    }

    public void Close()
    {
        if (panel) panel.SetActive(false);
    }
}