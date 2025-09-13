using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
   public Text playerNameText;  // «·‰’ «·–Ì ”ÌŸÂ— ›Êﬁ —√” «··«⁄»

    void Start()
    {
        if (playerNameText == null)
        {
            Debug.LogError("playerNameText is not assigned! Please assign it in the Inspector.");
            return; // ‰Êﬁ› «·”ﬂ—»  ≈–« ·„ Ì „ —»ÿ «·‹ Text
        }

        // «” —Ã«⁄ «·√”„«¡ „‰ PlayerPrefs
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        // «· Õﬁﬁ „‰ «· «ﬁ ·⁄—÷ «·«”„ «·„‰«”»
        if (gameObject.CompareTag("Player"))  // ≈–« ﬂ«‰ «·ﬂ«∆‰ ÌÕ„·  «ﬁ "Player"
        {
            playerNameText.text = player1Name;  // ⁄—÷ «”„ Player 1 ›Êﬁ —√”Â
        }
        else if (gameObject.CompareTag("Player2"))  // ≈–« ﬂ«‰ «·ﬂ«∆‰ ÌÕ„·  «ﬁ "Player2"
        {
            playerNameText.text = player2Name;  // ⁄—÷ «”„ Player 2 ›Êﬁ —√”Â
        }
    }
}