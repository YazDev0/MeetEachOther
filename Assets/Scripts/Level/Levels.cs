using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public string nextSceneName = "Level2";  // ÇÓã ÇáãÓÊæì ÇáÊÇáí

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ÊÍŞŞ ÅĞÇ ßÇä ÇáÚäÕÑ ÇáĞí ÊáÇãÓ ãÚ ÇááÇÚÈ åæ ÇááÇÚÈ ÇáÂÎÑ
        if (collision.CompareTag("Player"))
        {
            // ÇáÇäÊŞÇá Åáì ÇáãÓÊæì ÇáÊÇáí
            SceneManager.LoadScene(nextSceneName);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ÇáÇäÊŞÇá Åáì ÇáãÓÊæì ÇáÊÇáí ÚäÏ ÇáÊáÇãÓ Èíä ÇááÇÚÈíä
            SceneManager.LoadScene(nextSceneName);
        }
    }
}