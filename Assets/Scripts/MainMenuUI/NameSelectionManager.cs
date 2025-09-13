using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSelectionManager : MonoBehaviour
{
    public InputField player1NameInput;
    public InputField player2NameInput;

    public void StartGame()
    {
        // ����� ������� �� PlayerPrefs
        PlayerPrefs.SetString("Player1Name", player1NameInput.text);
        PlayerPrefs.SetString("Player2Name", player2NameInput.text);

        // ����� ������� (�������)
        PlayerPrefs.Save();

        // �������� ��� ���� ������
        UnityEngine.SceneManagement.SceneManager.LoadScene("LEVEL1");
    }
}