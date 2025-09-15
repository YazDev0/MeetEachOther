using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [SerializeField] TMP_Text ui;       
    public float Elapsed { get; private set; }
    bool running;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!running) return;
        Elapsed += Time.unscaledDeltaTime;
        if (ui) ui.text = Format(Elapsed);
    }

    public void ResetAndStart() { Elapsed = 0f; running = true; }
    public void Pause() { running = false; }
    public void Resume() { running = true; }

    public string StopAndGet()
    {
        running = false;
        SaveBest(Elapsed);
        return Format(Elapsed);
    }

    public static string Format(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        int ms = (int)((t - Mathf.Floor(t)) * 1000f);
        return $"{m:00}:{s:00}.{ms:000}";
    }

    void SaveBest(float t)
    {
        float best = PlayerPrefs.GetFloat("BEST_TIME", float.MaxValue);
        if (t < best)
        {
            PlayerPrefs.SetFloat("BEST_TIME", t);
            PlayerPrefs.Save();
        }
    }

    public string GetBestFormatted()
    {
        float best = PlayerPrefs.GetFloat("BEST_TIME", -1f);
        return best < 0 ? "--:--.---" : Format(best);
    }
}
