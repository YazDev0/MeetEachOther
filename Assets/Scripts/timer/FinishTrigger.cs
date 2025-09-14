using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;   // («Œ Ì«—Ì) ‰’ ·⁄—÷ «·‰ ÌÃ…
    [SerializeField] string nextScene = ""; // («Œ Ì«—Ì) «”„ «·”Ì¯‰ «· «·Ì
    [SerializeField] string playerTag1 = "Player";
    [SerializeField] string playerTag2 = "Player2";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag1) && !other.CompareTag(playerTag2)) return;

        string final = GameTimer.Instance ? GameTimer.Instance.StopAndGet() : "00:00.000";
        string best = GameTimer.Instance ? GameTimer.Instance.GetBestFormatted() : "--:--.---";

        if (resultText)
        {
            resultText.text = $"Time: {final}\nBest: {best}";
            resultText.gameObject.SetActive(true);
        }

        if (!string.IsNullOrEmpty(nextScene))
            SceneManager.LoadScene(nextScene);
    }
}