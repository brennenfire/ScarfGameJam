using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float decceleration = 1f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float airBreaking = 1f;
    [SerializeField] float airAcceleration = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpVelocity = 1f;
    [SerializeField] float downPull = 1f;
    [SerializeField] float boostMultiplier = 5f;
    [SerializeField] Transform feet;
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;

    new Rigidbody2D rigidbody;
    new Animator animation;
    SpriteRenderer spriteRenderer;
    public Vector2 startingPosition;
    string readHorizontal;
    string jumpButton;
    string climbButton;
    string boostButton;
    string wallJump;
    float horizontal;
    float fallTimer;
    bool isGrounded;
    int layerMaskGrounded;
    int jumpsRemaining = 1;
    bool falling = false;
    bool boost;
    //bool canWallJump = false;
    int boostCounter = 1;

    public Vector2 StartingPosition => startingPosition;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        readHorizontal = $"PHorizontal";
        jumpButton = $"PJump";
        climbButton = $"PClimb";
        boostButton = $"PBoost";
        wallJump = $"PWallJump";
        layerMaskGrounded = LayerMask.GetMask("Ground");
        startingPosition = transform.position;
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        MoveHorizontal();
        UpdateAnimator();
        FlipDirection();
        IsFalling();
        ShouldBoost();
        if(ShouldClimb()) 
        {
            //if(canWallJump) 
            //{
            WallJump();
            //}
            WallClimb();
        }
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
            var downForce = downPull * fallTimer;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y - downForce);
        }
      
    }

    void WallJump()
   {
        Debug.Log("walljump");
        if (Input.GetButtonDown(jumpButton))
        {
            rigidbody.velocity = new Vector2(-horizontal * jumpVelocity * 1.5f, jumpVelocity * 1.2f);

        }
   }

    void ShouldBoost()
    {
        var grapple = GetComponent<Grappling>();
        if (grapple.isGrappling && Input.GetButtonDown(boostButton) && boostCounter == 1)
        {
            boost = true;
        }
        else
        {
            boost = false;
        }
    }

    private void WallClimb()
    {
        //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 5);
    }

    void IsFalling()
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
        animation.SetBool("Walk", walking && isGrounded);
        animation.SetBool("Jump", !isGrounded && !falling && !grapple.isGrappling);
        animation.SetBool("Fall", falling && !grapple.isGrappling);
        //animation.SetBool("JumpWindup", ShouldStartJump());
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
        /*
        if(horizontal < 0)
        {
            transform.GetComponent<Collider2D>().offset += Vector2.right;
        }
        */
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
        if (isGrounded == false)
        {
            smoothnessMultiplier = horizontal == 0 ? airBreaking : airAcceleration;
        }
        float newHorizontal = Mathf.Lerp(rigidbody.velocity.x, horizontal * speed, Time.deltaTime * smoothnessMultiplier);
        if (!boost)
        {
            rigidbody.velocity = new Vector2(newHorizontal, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(newHorizontal * boostMultiplier, rigidbody.velocity.y);
            boostCounter--;
            StartCoroutine(WaitForRefresh());
        }
        
    }

    
    IEnumerator WaitForRefresh()
    {
        yield return new WaitForSeconds(0.5f);
        boostCounter++;
    }
   

    bool ShouldClimb()
    {
        //canWallJump = true;
        //var input = Input.GetButtonDown(climbButton);
        //if (input)
        //{
            if (isGrounded)
            {
                return false;
            }

            if (rigidbody.velocity.y > 0)
            {
                return false;
            }

            if (horizontal < 0)
            {
                var hit = Physics2D.OverlapCircle(leftSensor.position, 0.1f);
                if (hit != false && hit.CompareTag("Wall"))
                {
                    return true;
                }
            }
            if (horizontal > 0)
            {
                var hit = Physics2D.OverlapCircle(rightSensor.position, 0.1f);
                if (hit != false && hit.CompareTag("Wall"))
                {
                    return true;
                }
            }
            return false;

        //}
        //return false;
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMaskGrounded);
        isGrounded = hit != null;
    }

    public void ResetToStart()
    {
        transform.position = startingPosition;
    }


    public void HitLever()
    {
        var lever = GetComponent<Lever>();
    }
}
