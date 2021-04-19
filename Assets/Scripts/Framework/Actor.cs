using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool Active = true;
    public bool CCImmune = false;
    public GameObject pivotPoint;

    [SerializeField]
    protected int maxHealth = 0;
    protected int maxMana = 0;
    protected int currentHealth = 0;
    protected int currentMana = 0;
    
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    ///------------Utility Methods---------------///
    
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }
}
