using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.PlayMusic("Funky", 0.0f);
    }
}