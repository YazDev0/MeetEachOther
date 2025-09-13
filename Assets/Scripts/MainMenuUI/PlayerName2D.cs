using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
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
        // ÌáÈ ÇáÃÓãÇÁ ÇáãÍİæÙÉ
        string player1Name = PlayerPrefs.GetString("Player1Name", "Player 1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player 2");

        // ÅäÔÇÁ äÕ ãäİÕá (áíÓ ÇÈäğÇ ááÇÚÈ)
        nameText = new GameObject("PlayerName");

        // ÅÖÇİÉ ãßæä ÇáäÕ
        textMesh = nameText.AddComponent<TextMeshPro>();
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = fontSize;
        textMesh.fontStyle = FontStyles.Bold;
        textMesh.outlineWidth = 0.2f;
        textMesh.outlineColor = Color.black;

        // ÊÍÏíÏ Ãí áÇÚÈ åĞÇ ÈäÇÁğ Úáì ÇáÊÇÌ
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

        // ÊÚííä ãŞíÇÓ ËÇÈÊ ßÈíÑ
        nameText.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        if (nameText != null)
        {
            // ÊÍÏíË ãæŞÚ ÇáäÕ áíßæä İæŞ ÇááÇÚÈ (ÈÏæä Ãä íßæä ÇÈäğÇ)
            nameText.transform.position = transform.position + textOffset;

            // ÌÚá ÇáäÕ ËÇÈÊ ÊãÇãğÇ æáÇ íÏæÑ ãÚ ÇááÇÚÈ
            nameText.transform.rotation = Quaternion.identity; // ÏæÑÇä ËÇÈÊ

            // ÇáÊÃßÏ ãä Ãä ÇáãŞíÇÓ ËÇÈÊ æáÇ íäÚßÓ
            nameText.transform.localScale = new Vector3(
                Mathf.Abs(0.5f), // ãæÌÈ ÏÇÆãğÇ
                Mathf.Abs(0.5f),
                Mathf.Abs(0.5f)
            );
        }
    }

    void OnDestroy()
    {
        // ÊäÙíİ ÇáäÕ ÚäÏ ÊÏãíÑ ÇáßÇÆä
        if (nameText != null)
        {
            Destroy(nameText);
        }
    }
}