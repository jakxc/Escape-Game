using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Animator playerAnim;
    CapsuleCollider2D playerCollider;
    public bool isAlive;
    [SerializeField] int health = 3;
    float holdBreathTime = 0f;
    [SerializeField] float maxHoldBreathTime = 5f;

    void Awake() 
    {
       playerAnim = GetComponent<Animator>();
       playerCollider = GetComponent<CapsuleCollider2D>();
       holdBreathTime = maxHoldBreathTime;
       isAlive = true;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        ToggleAlive();    
    }

     void ToggleAlive() {
        // If player touches enemy or hazards, player is not alive
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) 
        {
            Die();
        }

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            holdBreathTime -= Time.deltaTime;

            if (holdBreathTime <= 0) {
               Die();
            }
        }
        else 
        {   
            if (holdBreathTime < maxHoldBreathTime) 
            {
                holdBreathTime += Time.deltaTime;
            }
        }
    }

    void Die()
    {
        isAlive = false;
        ModifyHealth(-1);
        playerAnim.SetTrigger("isDead");

        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    public int GetHealth()
    {
        return health;
    }

    public void ModifyHealth(int amount)
    {
        health += amount;
    }
}
