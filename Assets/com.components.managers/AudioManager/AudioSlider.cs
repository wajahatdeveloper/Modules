using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;
    public AudioManager.AudioChannel channel;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        AudioManager.Instance.SetVolume(value/100, channel);
        valueText.text = value.ToString();
        Debug.Log($"{channel} volume set to {value}");
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }
}