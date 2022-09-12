using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    Vector2 moveInput;  
    Rigidbody2D playerBody;
    SpriteRenderer playerSprite;
    Animator playerAnim;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    bool isAlive = true;
    float initialGravityScale;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    float holdBreathTime = 0f;
    [SerializeField] float maxHoldBreathTime = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform weapon;
    [SerializeField] AudioClip projectileSFX;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        initialGravityScale = playerBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {
            return;
        }

        Run();
        FlipSprite();
        Climb();
        ToggleAlive();
    }

    void OnMove(InputValue value) 
    {   
        if (!isAlive) {
            return;
        } 

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {   
         if (!isAlive) {
            return;
        }

        // If player is not on ground or climbing, do not jump
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing")))
        {
            return;
        }

        if (value.isPressed)
        {   
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire() {
        if (!isAlive) {
            return;
        }

        AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position);
        Instantiate(projectile, weapon.position, transform.rotation);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        playerAnim.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {   
        // MathF.Epsilon is used for potential values on velocity when player is idle
        bool isMoving = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

        if (isMoving) 
        {
            playerSprite.flipX = !(playerBody.velocity.x > 0);    
        }
    }

    void Climb()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            playerBody.gravityScale = initialGravityScale;
            playerAnim.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(playerBody.velocity.x, moveInput.y * climbSpeed);
        playerBody.velocity = climbVelocity;
        playerBody.gravityScale = 0f;
        
        bool hasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        playerAnim.SetBool("isClimbing", hasVerticalSpeed);
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
        playerAnim.SetTrigger("isDead");

        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

}
