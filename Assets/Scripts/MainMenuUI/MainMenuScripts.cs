using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    // ������ ���� ������ �������
    public void StartGame()
    {
        SceneManager.LoadScene("ChoseName"); // ��� ���� ������ �������
    }

    // ������ �� ������
    public void ExitGame()
    {
        Application.Quit(); // ���� �������
    }

    // �� ��� �� ������� ���� ��� ��� ��� ������ ��� ���� ���������
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene"); // ��� ��� �� ���� ���������
    }
}
