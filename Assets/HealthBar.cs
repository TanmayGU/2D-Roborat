using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthSlider;

    Damageable playerDamageable;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No player found");
        }

        playerDamageable = player.GetComponent<Damageable>();
    }
    void Start()
    {
        healthSlider.fillAmount = CalculateSliderFill(playerDamageable.Health , playerDamageable.MaxHealth); 
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculateSliderFill(float currentHealth, float maxHealth)
    {
       return currentHealth / maxHealth;
    }

    // Update is called once per frame
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.fillAmount = CalculateSliderFill(newHealth, maxHealth);
    }
}
