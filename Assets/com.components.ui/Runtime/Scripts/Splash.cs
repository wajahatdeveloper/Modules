using System.Collections;
using UnityEngine;

public class Splash : SingletonBehaviourUI<Splash>
{
	public float nextSceneDelay = 2.0f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(nextSceneDelay);
		Debug.Log("Splash: Loading next scene..");
		SceneManagerX.LoadNextScene();
	}
}