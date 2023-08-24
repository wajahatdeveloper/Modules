using UnityEngine;

public class DelayedActionHandler
{
    private float delayDuration;
    private float delayTimer;

    public DelayedActionHandler(float duration)
    {
        delayDuration = duration;
    }

    public bool CheckDelayedAction(System.Action action)
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delayDuration)
        {
            delayTimer = 0.0f;
            action.Invoke();
            return true;
        }
        
        return false;
    }
}