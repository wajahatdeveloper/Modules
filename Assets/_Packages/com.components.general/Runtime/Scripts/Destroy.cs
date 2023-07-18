using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
	public float delay = 0.0f;

	private void OnEnable()
	{
		if (delay > 0.0f)
		{
			Invoke(nameof(DestroyGameObject), delay);
		}
	}

	private void DestroyGameObject()
	{
		Destroy(gameObject);
	}
}