using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScripts : MonoBehaviour
{
    public Slider masterVolume;        
    public GameObject panel;

    [Header("AudioMixer")]
    public AudioMixer mixer;           
    public string exposedParam = "MasterVolume"; 

    const string VOL_KEY = "vol";

   
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    void Awake()
    {
        if (masterVolume) masterVolume.onValueChanged.AddListener(OnVolumeChanged);

      
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
       
        float dB = (v <= 0.0001f) ? -80f : Mathf.Log10(v) * 20f;
        if (mixer) mixer.SetFloat(exposedParam, dB);

      
        SetAllAudioSourcesVolume(v);

        PlayerPrefs.SetFloat(VOL_KEY, v);
        PlayerPrefs.Save();
    }

  
    void FindAllAudioSources()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        allAudioSources.Clear();
        allAudioSources.AddRange(sources);
    }

    
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

    
    public void OpenSettings()
    {
        if (panel) panel.SetActive(true);
        FindAllAudioSources(); 
    }

    public void Close()
    {
        if (panel) panel.SetActive(false);
    }
}