using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
   public Text playerNameText;  // ���� ���� ����� ��� ��� ������

    void Start()
    {
        if (playerNameText == null)
        {
            Debug.LogError("playerNameText is not assigned! Please assign it in the Inspector.");
            return; // ���� ������� ��� �� ��� ��� ��� Text
        }

        // ������� ������� �� PlayerPrefs
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        // ������ �� ����� ���� ����� �������
        if (gameObject.CompareTag("Player"))  // ��� ��� ������ ���� ��� "Player"
        {
            playerNameText.text = player1Name;  // ��� ��� Player 1 ��� ����
        }
        else if (gameObject.CompareTag("Player2"))  // ��� ��� ������ ���� ��� "Player2"
        {
            playerNameText.text = player2Name;  // ��� ��� Player 2 ��� ����
        }
    }
}