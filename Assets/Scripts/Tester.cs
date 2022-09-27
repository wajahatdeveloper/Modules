using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

public class Tester : MonoBehaviour
{
	public bool flag = false;

	private void Update()
	{
		if (flag)
		{
			Debug.Log("Flag Hoisted");
			flag = false;
		}
	}
}