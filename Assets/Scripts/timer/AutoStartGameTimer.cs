using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartGameTimer : MonoBehaviour
{
    void Start()
    {
        if (GameTimer.Instance != null)
            GameTimer.Instance.ResetAndStart();
    }
}
