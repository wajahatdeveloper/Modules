using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    public Toggle toggle;
    public AudioManager.AudioChannel channel;

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        AudioManager.Instance.SetVolume((value)?1:0, channel);
        Debug.Log($"{channel} volume set to {value}");
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnValueChanged);
    }
}