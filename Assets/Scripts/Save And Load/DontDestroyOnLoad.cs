using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // منع تدمير هذا الكائن عند تحميل مشهد جديد
        DontDestroyOnLoad(gameObject);
    }
}

