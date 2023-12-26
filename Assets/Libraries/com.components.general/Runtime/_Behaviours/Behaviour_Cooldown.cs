using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A class to handle cooldown related properties and their resource consumption over time
/// Remember to initialize it (once) and update it every frame from another class
/// </summary>
[System.Serializable]
public class Behaviour_Cooldown
{
	/// all possible states for the object
	public enum CooldownStates { Idle, Consuming, PauseOnEmpty, Refilling }
	/// if this is true, the cooldown won't do anything
	public bool Unlimited = false;
	/// the time it takes, in seconds, to consume the object
	public float ConsumptionDuration = 2f;
	/// the pause to apply before refilling once the object's been depleted
	public float PauseOnEmptyDuration = 1f;
	/// the duration of the refill, in seconds, if uninterrupted
	public float RefillDuration = 1f;
	/// whether or not the refill can be interrupted by a new Start instruction
	public bool CanInterruptRefill = true;
	[ReadOnly]
	/// the current state of the object
	public CooldownStates CooldownState = CooldownStates.Idle;
	[ReadOnly]
	/// the amount of duration left in the object at any given time
	public float CurrentDurationLeft;

	protected WaitForSeconds _pauseOnEmptyWFS;
	protected float _emptyReachedTimestamp = 0f;

	/// <summary>
	/// An init method that ensures the object is reset
	/// </summary>
	public virtual void Initialization()
	{
		_pauseOnEmptyWFS = new WaitForSeconds(PauseOnEmptyDuration);
		CurrentDurationLeft = ConsumptionDuration;
		CooldownState = CooldownStates.Idle;
		_emptyReachedTimestamp = 0f;
	}

	/// <summary>
	/// Starts consuming the cooldown object if possible
	/// </summary>
	public virtual void Start()
	{
		if (Ready())
		{
			CooldownState = CooldownStates.Consuming;
		}
	}

	public virtual bool Ready()
	{
		if (Unlimited)
		{
			return true;
		}
		if (CooldownState == CooldownStates.Idle)
		{
			return true;
		}
		if ((CooldownState == CooldownStates.Refilling) && (CanInterruptRefill))
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// Stops consuming the object
	/// </summary>
	public virtual void Stop()
	{
		if (CooldownState == CooldownStates.Consuming)
		{
			CooldownState = CooldownStates.PauseOnEmpty;
		}
	}

	public float Progress
	{
		get
		{
			if (Unlimited)
			{
				return 1f;
			}

			if (CooldownState == CooldownStates.Consuming || CooldownState == CooldownStates.PauseOnEmpty)
			{
				return 0f;
			}

			if (CooldownState == CooldownStates.Refilling) return /*Mathf.Clamp01(*/CurrentDurationLeft / RefillDuration/*)*/;

			return 1f; // refilled
		}
	}

	/// <summary>
	/// Processes the object's state machine
	/// </summary>
	public virtual void Update()
	{
		if (Unlimited)
		{
			return;
		}

		switch (CooldownState)
		{
			case CooldownStates.Idle:
				break;

			case CooldownStates.Consuming:
				CurrentDurationLeft = CurrentDurationLeft - UnityEngine.Time.deltaTime;
				if (CurrentDurationLeft <= 0f)
				{
					CurrentDurationLeft = 0f;
					_emptyReachedTimestamp = UnityEngine.Time.time;
					CooldownState = CooldownStates.PauseOnEmpty;
				}
				break;

			case CooldownStates.PauseOnEmpty:
				if (UnityEngine.Time.time - _emptyReachedTimestamp >= PauseOnEmptyDuration)
				{
					CooldownState = CooldownStates.Refilling;
				}
				break;

			case CooldownStates.Refilling:
				CurrentDurationLeft += (RefillDuration * UnityEngine.Time.deltaTime) / RefillDuration;
				if (CurrentDurationLeft >= RefillDuration)
				{
					CurrentDurationLeft = ConsumptionDuration;
					CooldownState = CooldownStates.Idle;
				}
				break;
		}
	}
}