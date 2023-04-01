using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Quartzified.EditorAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

public class Tester : MonoBehaviour
{
	[Tag]
	public string tag;

	[Layer]
	public int layer;

	private IEnumerator Start()
	{
		Fader.Instance.OnFadeFromBlackComplete.AddListener(() => Debug.Log("OnFadeFromBlackComplete"));
		Fader.Instance.OnFadeToBlackComplete.AddListener(() => Debug.Log("OnFadeToBlackComplete"));

		Fader.Instance.FadeFromBlack();
		yield return new WaitForSeconds(2.0f);
		Fader.Instance.FadeToBlack();

		ApiHelper.Get("https://www.google.com", result => { Debug.Log(result); }, error => { Debug.Log(error); },"");
	}
}