using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int targetFrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }

	private void OnEnable()
	{
	}

	private void OnHome( GameObject sender, object data )
	{
		Debug.Log( "Game Manager : Going to Home" );
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 1 );
	}

	private void OnRestart( GameObject sender, object data )
	{
		Debug.Log( "Game Manager : Game Restarting" );
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}

	private void OnResumed( GameObject sender, object data )
	{
		Debug.Log( "Game Manager : Game is Resumed" );
		Time.timeScale = 1.0f;
	}

	private void OnPaused( GameObject sender, object data )
	{
		Debug.Log( "Game Manager : Game is Paused" );
		Time.timeScale = 0.0f;
	}

	public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Debug.Log($"Time Scale Set To {timeScale}");
    }

	private void OnDisable()
	{
	}
}