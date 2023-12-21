using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScrollableListItem : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI subtitle;
    public Image image;
    public Button button;

    public virtual void DoUpdate()
    {
    }
}