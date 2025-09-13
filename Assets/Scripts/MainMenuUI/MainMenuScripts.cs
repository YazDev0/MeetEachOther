using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    // áÊÍãíá ãÔåÏ ÇÎÊíÇÑ ÇáÃÓãÇÁ
    public void StartGame()
    {
        SceneManager.LoadScene("ChoseName"); // ÇÓã ãÔåÏ ÇÎÊíÇÑ ÇáÇÓãÇÁ
    }

    // ÇáÎÑæÌ ãä ÇááÚÈÉ
    public void ExitGame()
    {
        Application.Quit(); // íÛáŞ ÇáÊØÈíŞ
    }

    // áæ ßÇä İí ÅÚÏÇÏÇÊ íãßä æÖÚ ßæÏ åäÇ ááÊäŞá Åáì ãÔåÏ ÇáÅÚÏÇÏÇÊ
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene"); // ÅĞÇ ßÇä İí ãÔåÏ ááÅÚÏÇÏÇÊ
    }
}
