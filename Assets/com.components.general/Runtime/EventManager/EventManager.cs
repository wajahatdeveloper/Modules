using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
	// Gameplay Events
	public UnityEvent OnVictory;
	public UnityEvent OnDefeat;

	// Game Events
	public UnityEvent OnGameStart;
	public UnityEvent OnGameRestart;
	public UnityEvent OnGameExit;

	// Pause and Resume
	public UnityEvent OnGameResume;
	public UnityEvent OnGamePause;
}