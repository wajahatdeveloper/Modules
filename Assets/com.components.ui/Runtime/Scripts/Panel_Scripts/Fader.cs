using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : SingletonBehaviourUI<Fader>
{
	public Image fadeImage;
	[Space]
	public UnityEvent OnFadeToBlackComplete;
	public UnityEvent OnFadeFromBlackComplete;

	private bool isFadeToBlack = false;

	[ContextMenu(nameof(FadeToBlack))]
	public void FadeToBlack(float duration = 1.0f)
	{
		isFadeToBlack = true;
		StartCoroutine(Fade(0, 1, duration));
	}

	[ContextMenu(nameof(FadeFromBlack))]
	public void FadeFromBlack(float duration = 1.0f)
	{
		isFadeToBlack = false;
		StartCoroutine(Fade(1, 0, duration));
	}

	private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
	{
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float normalizedTime = t / duration;
			//right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
			fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, endAlpha, normalizedTime));
			yield return null;
		}

		fadeImage.color = new Color(0, 0, 0, endAlpha); ; //without this, the value will end at something like 0.9992367

		if (isFadeToBlack)
		{
			OnFadeToBlackComplete?.Invoke();
		}
		else
		{
			OnFadeFromBlackComplete?.Invoke();
		}
	}
}