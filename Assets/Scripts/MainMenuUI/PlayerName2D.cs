using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
public class PlayerName2D : MonoBehaviour
{


    void Start()
    {
        // Ã·» «·√”„«¡ «·„Õ›ÊŸ…
        string player1Name = PlayerPrefs.GetString("Player1Name", "«··«⁄» 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "«··«⁄» 2");

        // ≈‰‘«¡ ‰’ ›Êﬁ —√” «··«⁄»
        GameObject nameText = new GameObject("PlayerName");
        nameText.transform.SetParent(transform);
        nameText.transform.localPosition = new Vector3(0, 1.5f, 0);

        // ≈÷«›… „ﬂÊ‰ «·‰’
        TextMeshPro textMesh = nameText.AddComponent<TextMeshPro>();
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 3;

        //  ÕœÌœ √Ì ·«⁄» Â–« »‰«¡ ⁄·Ï «· «Ã
        if (gameObject.CompareTag("Player"))
        {
            textMesh.text = player1Name;
            textMesh.color = Color.blue;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            textMesh.text = player2Name;
            textMesh.color = Color.red;
        }
    }
}