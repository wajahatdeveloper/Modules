using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    // The amount of health to begin with
    public float maxHealth = 100;
    public float currentHealth;

    public float disableDelay = 0.5f;
    public bool disableOnZeroHealth = true;
    public bool isImmune;

    public Health redirect;

    public UnityEvent onHealthReachZero;
    public UnityEvent<int,int> onHealthChange;      // Current Amount, ChangeDelta
    public IMedicTent medicTentManager;
    public static IAgentGroup agentGroupManager;
    /// <summary>
    /// Initailzies the current health.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetMedicTentInstance( IMedicTent medicTent ) => medicTentManager = medicTent;
    public static void GetAgentGroupInstance( IAgentGroup agentGroup ) => agentGroupManager = agentGroup;

    [Button]
    /// <summary>
    /// Take damage. Deactivate if the amount of remaining health is 0.
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(float amount)
    {
        if( isImmune ) return;

        if (redirect != null)
        {
            redirect.Damage(amount);
            return;
        }

        currentHealth = Mathf.Max(currentHealth - amount, 0);

        onHealthChange?.Invoke((int)currentHealth, (int)amount);

        if (currentHealth == 0)
        {
            if (disableOnZeroHealth)
            {
                Invoke(nameof(DisableGameobject),disableDelay);
            }

            if (gameObject.CompareTag("Ally") )
            {
                if (medicTentManager != null)
                {
                    medicTentManager.CallAvailableMedic( this );
                }
            }

            agentGroupManager.CheckIfAllEnemiesAreDead();
            onHealthReachZero?.Invoke();
        }
    }

    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }

    // Is the object alive?
    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    /// <summary>
    /// Sets the current health to the starting health and enables the object.
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        onHealthChange?.Invoke((int)currentHealth, (int)maxHealth);
        gameObject.SetActive(true);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        onHealthChange?.Invoke((int)currentHealth, (int)healAmount);
    }
}

public interface IMedicTent
{
    public void CallAvailableMedic( Health health );
}
public interface IAgentGroup
{
    public void CheckIfAllEnemiesAreDead();
}