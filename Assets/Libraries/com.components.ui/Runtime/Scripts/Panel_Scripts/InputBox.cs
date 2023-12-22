using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputBox : SingletonBehaviour<InputBox>
{
    public TextMeshProUGUI headingText;
    public TMP_InputField inputField;
    public GameObject inputPanel;

    [Header("Debugging")]
    public string enteredText = "";

    public UnityEvent onClose;
    public UnityEvent<string> onSubmit;

    public void Show(string message)
    {
        headingText.text = message;
        inputPanel.SetActive(true);
    }

    public void OnClick_Submit()
    {
        enteredText = inputField.text;
        onSubmit?.Invoke(enteredText);
        onSubmit?.RemoveAllListeners();
        Hide();
    }

    public void Hide()
    {
        inputPanel.SetActive(false);
        onClose?.Invoke();
        onClose?.RemoveAllListeners();
    }
}