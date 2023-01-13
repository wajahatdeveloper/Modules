using System;
using UnityEngine;

public static class MonoBehaviourExtensions
{
	public static void RunOnce(this MonoBehaviour behaviour, Action action)
    {
        var customState = behaviour.gameObject.AddOrGetComponent<CustomState>();
		customState.keyValuePairs.TryGetValue(action.ToString(),out var value);
		if (value != null)
        {
			return;
        }
		else
        {
			customState.keyValuePairs[action.ToString()] = "1";
			action();
		}
    }

	public static void RunOncePersistent(this MonoBehaviour behaviour, Action action)
	{
        if (PlayerPrefs.GetString(action.ToString(),"") != "")
        {
			return;
        }
		else
        {
			PlayerPrefs.SetString(action.ToString(), "1");
			action();
		}
	}

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