using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinningStatsUI : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text bestText;

    void Start()
    {
        if (GameTimer.Instance != null)
        {
            timeText.text = "Time: " + GameTimer.Format(GameTimer.Instance.Elapsed);
            bestText.text = "Best: " + GameTimer.Instance.GetBestFormatted();
        }
        else
        {
            timeText.text = "Time: --:--.---";
            bestText.text = "Best: --:--.---";
        }
    }
}