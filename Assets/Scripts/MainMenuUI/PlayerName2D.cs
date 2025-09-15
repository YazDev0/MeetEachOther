using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName2D : MonoBehaviour
{
    public float fontSize = 15f;
    public Vector3 textOffset = new Vector3(0, 2f, 0);
    private GameObject nameText;
    private TextMeshPro textMesh;

    void Start()
    {
        CreateNameTag();
    }

    void CreateNameTag()
    {
       
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

      
        nameText = new GameObject("PlayerName");

       
        textMesh = nameText.AddComponent<TextMeshPro>();
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = fontSize;
        textMesh.fontStyle = FontStyles.Bold;
        textMesh.outlineWidth = 0.2f;
        textMesh.outlineColor = Color.black;

       
        if (gameObject.CompareTag("NameTag"))
        {
            textMesh.text = player1Name;
            textMesh.color = Color.blue;
        }
        else if (gameObject.CompareTag("NameTag2"))
        {
            textMesh.text = player2Name;
            textMesh.color = Color.red;
        }

        
        nameText.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        if (nameText != null)
        {
            
            nameText.transform.position = transform.position + textOffset;

           
            nameText.transform.rotation = Quaternion.identity; 

            
            nameText.transform.localScale = new Vector3(
                Mathf.Abs(0.5f), 
                Mathf.Abs(0.5f),
                Mathf.Abs(0.5f)
            );
        }
    }

    void OnDestroy()
    {
        
        if (nameText != null)
        {
            Destroy(nameText);
        }
    }
}