using UnityEngine;

public static class MonoBehaviourExtensions
{
	/// <summary>
	/// disable the specified behaviour if the assert value is false, and throw a warning
	/// </summary>
	/// <param name="behaviour"></param>
	/// <param name="assertValue"></param>
	/// <param name="message"></param>
	public static void Assert(this MonoBehaviour behaviour, bool assertValue, string message = "")
	{
		if (!assertValue)
		{
			Debug.LogWarning("Assert failed. " + message);
			behaviour.enabled = false;
		}
	}
}