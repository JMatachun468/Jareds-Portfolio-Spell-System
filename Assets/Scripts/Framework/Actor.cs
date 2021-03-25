using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool Active = true;
    public bool CCImmune = false;

    [SerializeField]
    protected int maxHealth = 0;
    protected int maxMana = 0;
    protected int currentHealth = 0;
    protected int currentMana = 0;
    
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
    }


    ///------------Utility Methods---------------///
    
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int getMaxMana()
    {
        return maxMana;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentMana()
    {
        return currentMana;
    }
}
