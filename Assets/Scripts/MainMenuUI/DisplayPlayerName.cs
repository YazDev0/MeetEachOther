using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerName : MonoBehaviour
{
    public Text playerNameText;  // ÇáäÕ ÇáĞí ÓíÙåÑ İæŞ ÑÃÓ ÇááÇÚÈ

    void Start()
    {
        // ÇÓÊÑÌÇÚ ÇáÃÓãÇÁ ãä PlayerPrefs
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        // ÊÚííä ÇáÃÓãÇÁ ááÇÚÈíä ÍÓÈ ÇáßÇÆä
        if (gameObject.name == "Player1")
        {
            playerNameText.text = player1Name;  // ÚÑÖ ÇÓã Player 1 İæŞ ÑÃÓå
        }
        else if (gameObject.name == "Player2")
        {
            playerNameText.text = player2Name;  // ÚÑÖ ÇÓã Player 2 İæŞ ÑÃÓå
        }
    }
}