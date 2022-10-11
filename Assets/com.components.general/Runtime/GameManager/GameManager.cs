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

	private void StartGame()
	{
		Debug.Log( "Game Manager : Start Game.." );
		EventManager.Instance.OnGameStart?.Invoke();
	}

	private void ExitGame()
	{
		Debug.Log("Game Manager : Exit Game..");
		EventManager.Instance.OnGameExit?.Invoke();
	}

	private void RestartGame()
	{
		Debug.Log( "Game Manager : Game Restart.." );
		EventManager.Instance.OnGameRestart?.Invoke();
	}

	private void ResumeGame()
	{
		Debug.Log( "Game Manager : Game is Resumed" );
		Time.timeScale = 1.0f;
		EventManager.Instance.OnGameResume?.Invoke();
	}

	private void PauseGame()
	{
		Debug.Log( "Game Manager : Game is Paused" );
		Time.timeScale = 0.0f;
		EventManager.Instance.OnGamePause?.Invoke();
	}

	public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Debug.Log($"Time Scale Set To {timeScale}");
    }

	public void GameVictory()
	{
		Debug.Log("Game Manager : Game Victory");
		EventManager.Instance.OnVictory?.Invoke();
	}

	public void GameDefeat()
	{
		Debug.Log("Game Manager : Game Defeat");
		EventManager.Instance.OnDefeat?.Invoke();
	}

	private void OnDisable()
	{
	}
}