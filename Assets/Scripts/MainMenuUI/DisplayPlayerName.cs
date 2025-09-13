using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerName : MonoBehaviour
{
    public Text playerNameText;  // ���� ���� ����� ��� ��� ������

    void Start()
    {
        // ������� ������� �� PlayerPrefs
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        // ����� ������� ������� ��� ������
        if (gameObject.name == "Player1")
        {
            playerNameText.text = player1Name;  // ��� ��� Player 1 ��� ����
        }
        else if (gameObject.name == "Player2")
        {
            playerNameText.text = player2Name;  // ��� ��� Player 2 ��� ����
        }
    }
}