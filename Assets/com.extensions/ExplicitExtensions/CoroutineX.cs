using System.Collections;
using UnityEngine;

	public static class CoroutineX
	{
		private static CoroutineOwner CoroutineOwner
		{
			get
			{
				if (_coroutineOwner != null) return _coroutineOwner;

				var go = new GameObject("Static Coroutine Owner");
				Object.DontDestroyOnLoad(go);
				go.hideFlags = HideFlags.HideAndDontSave;

				_coroutineOwner = go.AddComponent<CoroutineOwner>();

				return _coroutineOwner;
			}
		}

		private static CoroutineOwner _coroutineOwner;
		
		/// <summary>
		/// StartCoroutine without MonoBehaviour
		/// </summary>
		public static Coroutine StartCoroutine(this IEnumerator coroutine)
		{
			return CoroutineOwner.StartCoroutine(coroutine);
		}

		/// <summary>
		/// Stop coroutine started with MyCoroutines.StartCoroutine
		/// </summary>
		public static void StopCoroutine(Coroutine coroutine)
		{
			CoroutineOwner.StopCoroutine(coroutine);
		}

		/// <summary>
		/// Stop all coroutines started with MyCoroutines.StartCoroutine
		/// </summary>
		public static void StopAllCoroutines()
		{
			CoroutineOwner.StopAllCoroutines();
		}
	}

internal class CoroutineOwner : MonoBehaviour
{
}
