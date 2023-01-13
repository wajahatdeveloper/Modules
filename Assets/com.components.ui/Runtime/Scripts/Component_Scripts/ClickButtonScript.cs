using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonScript : MonoBehaviour
{
	public Button button;

	public void Click()
	{
		button.onClick?.Invoke();
	}
}
