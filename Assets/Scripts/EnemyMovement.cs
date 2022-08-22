using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    Rigidbody2D enemyBody;
    SpriteRenderer enemySprite;

    [SerializeField] int enemyValue = 2;
    [SerializeField] float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyBody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;  
        FlipSprite();  
    }

    void FlipSprite() 
    {   
        if (enemyBody.velocity.x > 0)
        {
            enemySprite.flipX = true;
        } 
        else 
        {
            enemySprite.flipX = false;
        }  
    }

    public int GetEnemyValue()
    {
        return enemyValue;
    }
}
