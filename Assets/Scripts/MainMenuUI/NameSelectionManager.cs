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
        //  Œ“Ì‰ «·√”„«¡ ›Ì PlayerPrefs
        PlayerPrefs.SetString("Player1Name", player1NameInput.text);
        PlayerPrefs.SetString("Player2Name", player2NameInput.text);

        //  √ﬂÌœ «· Œ“Ì‰ («Œ Ì«—Ì)
        PlayerPrefs.Save();

        // «·«‰ ﬁ«· ≈·Ï „‘Âœ «··⁄»…
        UnityEngine.SceneManagement.SceneManager.LoadScene("LEVEL1");
    }
}