using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI text;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float value)
    {
        currentHealth -= value;
        healthBar.value = currentHealth / maxHealth;
        text.text = currentHealth + "/" + maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("Game");
    }
}
