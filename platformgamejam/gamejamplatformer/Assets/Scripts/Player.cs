using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float decceleration = 1f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpVelocity = 1f;
    [SerializeField] float downPull = 1f;
    [SerializeField] Transform feet;

    new Rigidbody2D rigidbody;
    new Animator animation;
    SpriteRenderer spriteRenderer;
    string readHorizontal;
    string jumpButton;
    string attackButton;
    float horizontal;
    bool isGrounded;
    int layerMask;
    int jumpsRemaining = 1;
    bool falling = false;
    float fallTimer;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        readHorizontal = $"PHorizontal";
        jumpButton = $"PJump";
        layerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        MoveHorizontal();
        UpdateAnimator();
        FlipDirection();
        isFalling();
        if (ShouldStartJump() && isGrounded)
        {
            Jump();
        }
        if (isGrounded && fallTimer > 0)
        {
            falling = false;
            fallTimer = 0;
            jumpsRemaining = 1;
        }
        else
        {
            fallTimer = Time.deltaTime;
            var downForce = downPull * fallTimer * fallTimer;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y - downForce);
        }
      
    }

    void isFalling()
    {
        if(rigidbody.velocity.y < -1f) 
        {
            falling = true;
        }
    }

    void FlipDirection()
    {
        spriteRenderer.flipX = horizontal < 0;
    }

    void UpdateAnimator()
    {
        var grapple = GetComponent<Grappling>();
        bool walking = horizontal != 0;
        animation.SetBool("Walk", walking);
        animation.SetBool("Jump", !isGrounded && !falling && !grapple.isGrappling);
        animation.SetBool("Fall", falling && !grapple.isGrappling);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
        jumpsRemaining--;
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(jumpButton) && jumpsRemaining > 0;
    }

    void ReadHorizontalInput()
    {
        horizontal = Input.GetAxis(readHorizontal) * speed;
    }
    void MoveHorizontal()
    {
        float smoothnessMultiplier = horizontal == 0 ? decceleration : acceleration;
        float newHorizontal = Mathf.Lerp(rigidbody.velocity.x, horizontal * speed, Time.deltaTime * smoothnessMultiplier);
        rigidbody.velocity = new Vector2(newHorizontal, rigidbody.velocity.y);
        
    }
    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
        isGrounded = hit != null;
    }
}
