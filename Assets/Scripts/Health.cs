using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int currentHealth;

    [SerializeField] int initialHealth = 3;
    
    public int GetHealth()
    {
        return currentHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void ResetHealth()
    {
        currentHealth = initialHealth;
    }
}
