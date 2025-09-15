using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public string gameSceneName = "ChoseName";

    void Start()
    {
        Time.timeScale = 1f;
        if (mainMenuPanel) mainMenuPanel.SetActive(true);
        if (settingsPanel) settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        if (mainMenuPanel) mainMenuPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (mainMenuPanel) mainMenuPanel.SetActive(true);

    }
        public void ExitGame()
    {
        Application.Quit(); 
    } 
} 
