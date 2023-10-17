using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Health bar")]
    [SerializeField]
    private Slider healthSlider;
    [SerializeField] 
    private Image healthFill;
    [SerializeField]
    private Gradient healthGradient;

    private int maxHealthValue;

    [SerializeField]
    private HealthManagerScriptableObject healthManager;


    [Header("Enemy Counter")]
    [SerializeField]
    private TextMeshProUGUI enemyCounterText;
    [SerializeField]
    private EnemyCounterScriptableObject enemyCounter;
    

    private void Start()
    {
        maxHealthValue = healthManager.actualMaxHealth;
        SetMaxHealth();
    }

    private void OnEnable()
    {
        healthManager.healthChangeEvent.AddListener(SetHealth);
        enemyCounter.enemyKilled.AddListener(UpdateEnemyCounter);

    }

    private void OnDisable()
    {
        healthManager?.healthChangeEvent.RemoveListener(SetHealth);
        enemyCounter.enemyKilled?.RemoveListener(UpdateEnemyCounter);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthSlider.maxValue = maxHealthValue;

        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = maxHealthValue;
        healthSlider.value = maxHealthValue;

        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);

    }

    public void UpdateEnemyCounter(int count)
    {
        enemyCounterText.text = count + " / " + enemyCounter.maxEnemies; 
    }

}
