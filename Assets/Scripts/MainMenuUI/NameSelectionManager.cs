using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameSelectionManager : MonoBehaviour
{
    public TMP_InputField player1Input;
    public TMP_InputField player2Input;
    public Button startButton;

    void Start()
    {
        // ��� �� ����� �� ���� ��� �������
        startButton.onClick.AddListener(SavePlayerNames);
    }

    void SavePlayerNames()
    {
        // ��� ��� ������ �����
        string player1Name = player1Input.text;
        if (string.IsNullOrEmpty(player1Name))
        {
            player1Name = "������ 1";
        }
        PlayerPrefs.SetString("Player1Name", player1Name);

        // ��� ��� ������ ������
        string player2Name = player2Input.text;
        if (string.IsNullOrEmpty(player2Name))
        {
            player2Name = "������ 2";
        }
        PlayerPrefs.SetString("Player2Name", player2Name);

        // �������� ��� ���� level1
        SceneManager.LoadScene("Level1");
    }
}