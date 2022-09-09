using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{      
    [SerializeField] float projectileSpeed = 10f;
    
    GameObject player;
    Rigidbody2D projRigidBody;
    
    float xSpeed;
    bool hasHit = false;

    // Start is called before the first frame update
    void Start()
    {
        projRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        if(player.GetComponent<SpriteRenderer>().flipX)
        {
            xSpeed = -projectileSpeed;
        }
        else 
        {
            xSpeed = projectileSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        projRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy" && !hasHit)
        {   
            hasHit = true;
            
            // Update player score with enemy value
            int enemyValue = other.GetComponent<EnemyMovement>().GetEnemyValue();
            FindObjectOfType<ScoreKeeper>().ModifyScore(enemyValue);
            
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
