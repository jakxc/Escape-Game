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

    Health playerHealth;
    bool enableInput;
    float initialGravityScale;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform weapon;
    [SerializeField] AudioClip projectileSFX;

    float holdBreathTime;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerHealth = GetComponent<Health>();
        feetCollider = GetComponent<BoxCollider2D>();
        initialGravityScale = playerBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        enableInput = playerHealth.isAlive;

        if (!enableInput) {
            return;
        }

        Run();
        FlipSprite();
        Climb();
    }

    void OnMove(InputValue value) 
    {   
        if (!enableInput) {
            return;
        } 

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {   
         if (!enableInput) {
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
        if (!enableInput) {
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
}
