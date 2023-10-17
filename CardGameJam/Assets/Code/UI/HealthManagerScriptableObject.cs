using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthManagerScriptableObject", menuName = "ScriptableObjects/ Health Manager")]
public class HealthManagerScriptableObject : ScriptableObject
{
    public int health = 100;
    public int roundHealthBonus = 20;
    [SerializeField]
    private int maxHealth = 100;

    [HideInInspector]
    public int actualMaxHealth;

    public UnityEvent<int> healthChangeEvent;


    private void OnEnable()
    {
        SetInitialHealth();

        if (healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<int>();
        }
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        healthChangeEvent.Invoke(health);
    }

    public void IncreaseHealth()
    {
        maxHealth += roundHealthBonus;
        health = maxHealth;
        healthChangeEvent.Invoke(health);
    }

    public void SetInitialHealth()
    {
        actualMaxHealth = maxHealth;
        health = actualMaxHealth;
        healthChangeEvent.Invoke(health);
    }
}
