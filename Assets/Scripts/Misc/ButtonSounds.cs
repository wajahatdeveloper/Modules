using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource clickSource;
    public AudioClip clickSound;

    private void OnEnable()
    {
        var buttons = GameObject.FindObjectsOfType<Button>(true);
        foreach (var button in buttons)
        {
            button.onClick.AddListener(ClickSound);
        }
    }

    private void ClickSound()
    {
        clickSource.PlayOneShot(clickSound);
    }
}