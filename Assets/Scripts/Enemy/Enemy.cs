using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [Header("Enemy Stats")]
    public int strength;
    public int stamina;
    public int intellect;
    public int spirit;
    public int agility;

    private void Start()
    {
        maxHealth = stamina * 10;
        maxMana = intellect * 10;
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }
}
