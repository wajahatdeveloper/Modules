using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
	public float delay = 0.0f;
	public bool isRealtime = false;

	private void OnEnable()
	{
		if (delay > 0.0f)
		{
			StartCoroutine(DestroyGameObject());
		}
	}

	private IEnumerator DestroyGameObject()
	{
		if (isRealtime)
		{
			yield return new WaitForSecondsRealtime(delay);
		}
		else
		{
			yield return new WaitForSeconds(delay);
		}
		
		Destroy(gameObject);
	}
}