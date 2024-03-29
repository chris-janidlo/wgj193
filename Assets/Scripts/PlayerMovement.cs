﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;
using UnityAtoms.BaseAtoms;
using crass;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    public struct BasicMovementProfile
    {
        // acceleration is how fast your speed changes while a button is held down, and deceleration is how quickly your speed returns back to zero. each are in units per second
        public float MaxSpeed, Acceleration, Deceleration;
    }

    [Header("Stats")]
    public float Gravity;
    public float MaxFallSpeed;
    public BasicMovementProfile GroundProfile, AirProfile, GlideProfile;

    // burst: initial instantaneous speed value when jumping
    // cut: when you release the jump button, your vertical speed is set to this if it's currently higher. essentially an air control value
    public float JumpSpeedBurst, JumpSpeedCut;
    public float EarlyJumpPressTime; // you can input jump this many seconds before landing and it will still count
    public float NonGroundedJumpGracePeriod; // same as above, but in seconds after falling off a ledge
    
    public float MinFallSpeedToStartGliding, MaxFallSpeedWhenGliding;

    public float DashSpeed, DashMovementBypassTime;

    public float SuperJumpBurst, SpeedToEndSuperJumpState;

    public float HalfHeight;
    public Vector2 GroundCheckBoxDimensions; // should typically be set to x = player width plus walking over platform distance, y = vertical fudge
    public ContactFilter2D GroundCheckFilter, PlatformStickFilter;

    [Header("Animation")]
    public string HorizontalIntName;
    public string VerticalIntName, GroundedBoolName;

    [Header("References")]
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public PlayerAbilityCharges PlayerAbilityCharges;
    public Vector3Variable CurrentPlayerPosition;

    bool groundedRecentlyEnoughToJump => nonGroundedGracePeriodTimer < NonGroundedJumpGracePeriod;

    Vector2 velocity;

    Vector2 moveInput;
    float horizontalMoveInputMemory;
    bool jumpInput, glideInput, dashInput, superJumpInput;
        
    bool grounded, gliding, jumping, superJumping;

    float earlyJumpPressTimer, nonGroundedGracePeriodTimer, dashMovementBypassTimer;

    // keep this at class-level to avoid making a new array every frame
    RaycastHit2D[] groundedHitList;

    void Start ()
    {
        groundedHitList = new RaycastHit2D[1];
        horizontalMoveInputMemory = 1;
    }

    void Update ()
    {
        earlyJumpPressTimer = Mathf.Max(earlyJumpPressTimer - Time.deltaTime, 0);
        nonGroundedGracePeriodTimer = Mathf.Min(nonGroundedGracePeriodTimer + Time.deltaTime, NonGroundedJumpGracePeriod);
        dashMovementBypassTimer = Mathf.Max(dashMovementBypassTimer - Time.deltaTime, 0);

        animate();
    }

    void FixedUpdate ()
    {
        platform();
    }

    public void OnMoveInput (CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0) horizontalMoveInputMemory = moveInput.x;
    }

    public void OnJumpInput (CallbackContext context)
    {
        if (context.performed)
        {
            earlyJumpPressTimer = EarlyJumpPressTime;
        }

        jumpInput = context.ReadValueAsButton();
    }

    public void OnSpecialInput (CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            if (grounded)
            {
                superJumpInput = context.performed;
            }
            else
            {
                glideInput = context.performed;
            }
        }
        else
        {
            // technically the action has successfully performed by here, but because it's a press it's already on to cancelled. since I manage the state of it later anyway, I'm just going to set it to true regardless here
            dashInput = true;
        }
    }

    void animate ()
    {
        SpriteRenderer.flipX = horizontalMoveInputMemory < 0;

        Animator.SetInteger(HorizontalIntName, (int) ternarySign(velocity.x));
        Animator.SetInteger(VerticalIntName, (int) ternarySign(velocity.y));
        Animator.SetBool(GroundedBoolName, grounded);
    }

    void platform ()
    {
        grounded = isGrounded();

        if (grounded) nonGroundedGracePeriodTimer = 0;

        stickToPlatforms();

        fall();
        dash(); // dash goes after fall so that fall doesn't cancel an upward, grounded dash. it goes before jump so it can turn off jumping as needed
        superJump(); // super jump before jump since super jump overrides jump
        jump();
        move();

        transform.position += (Vector3) velocity * Time.deltaTime;
        CurrentPlayerPosition.Value = transform.position;
    }

    void stickToPlatforms ()
    {
        if (Physics2D.BoxCast(transform.position, GroundCheckBoxDimensions, 0, Vector2.down, PlatformStickFilter, groundedHitList, HalfHeight) != 0)
        {
            if (transform.parent == null)
            {
                transform.parent = groundedHitList[0].transform;
                groundedHitList[0].collider.GetComponent<Platform>().PlayerLandedHere.Invoke();
            }
        }
        else if (transform.parent != null)
        {
            transform.parent = null;
        }
    }

    void fall ()
    {
        if (grounded)
        {
            velocity = new Vector2(velocity.x, 0);
            return;
        }

        if (dashMovementBypassTimer > 0) return;

        velocity += Vector2.down * Gravity * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, -MaxFallSpeed);


        if (!gliding && PlayerAbilityCharges.Glide > 0 && velocity.y < -MinFallSpeedToStartGliding && glideInput)
        {
            gliding = true;
            jumping = false; // so you can jump out of a glide
            PlayerAbilityCharges.Glide--;
        }

        if (gliding)
        {
            velocity = new Vector2
            (
                velocity.x,
                Mathf.Max(velocity.y, -MaxFallSpeedWhenGliding)
            );

        }

        if (!glideInput || grounded) gliding = false;
    }

    void jump ()
    {
        if (superJumping) return;

        float y = velocity.y;

        if ((groundedRecentlyEnoughToJump || PlayerAbilityCharges.ExtraJump > 0) && !jumping && earlyJumpPressTimer > 0)
        {
            // start of jump
            y = JumpSpeedBurst;
            jumping = true;
            gliding = false; // if you jump out of a glide
            earlyJumpPressTimer = 0;

            if (!grounded) PlayerAbilityCharges.ExtraJump--;
        }
        else if (grounded && jumping && y <= 0)
        {
            // landing
            jumping = false;
        }
        else if (jumping && !jumpInput)
        {
            // letting go of jump
            if (y > JumpSpeedCut) y = JumpSpeedCut; // jump height control
            jumping = false; // so you can always extra jump after letting go
        }

        velocity = new Vector2(velocity.x, y);
    }

    void move ()
    {
        if (dashMovementBypassTimer > 0) return;

        float x = velocity.x;
        float inputX = moveInput.x;

        BasicMovementProfile profile;

        if (gliding) profile = GlideProfile;
        else if (!grounded) profile = AirProfile;
        else profile = GroundProfile;

        if (inputX == 0)
        {
            Vector2 deceleration = Mathf.Sign(x) * Vector2.right * profile.Deceleration * Time.deltaTime;
            deceleration = Vector2.ClampMagnitude(deceleration, Mathf.Abs(x));
            x -= deceleration.x;
        }
        else
        {
            float acceleration = profile.Acceleration;

            if (x != 0 && ternarySign(inputX) != ternarySign(x))
            {
                // if we're switching directions, and the deceleration is faster, use the deceleration
                acceleration = Mathf.Max(profile.Acceleration, profile.Deceleration);
            }

            x = Mathf.Clamp
            (
                x + inputX * acceleration * Time.deltaTime,
                -profile.MaxSpeed,
                profile.MaxSpeed
            );
        }

        velocity = new Vector2(x, velocity.y);
    }

    void dash ()
    {
        if (dashInput && PlayerAbilityCharges.Dash > 0 && dashMovementBypassTimer == 0)
        {
            jumping = false;
            dashInput = false;
            PlayerAbilityCharges.Dash--;
            dashMovementBypassTimer = DashMovementBypassTime;

            Vector2 direction = moveInput;

            if (moveInput == Vector2.zero)
            {
                direction.x = Mathf.Sign(horizontalMoveInputMemory);
            }

            velocity = direction.normalized * DashSpeed;
        }
    }

    void superJump ()
    {
        if (groundedRecentlyEnoughToJump && superJumpInput && PlayerAbilityCharges.SuperJump > 0)
        {
            superJumping = true;
            PlayerAbilityCharges.SuperJump--;

            velocity = new Vector2(velocity.x, SuperJumpBurst);
        }

        if (superJumping && !groundedRecentlyEnoughToJump && velocity.y <= SpeedToEndSuperJumpState)
        {
            superJumping = false;
        }

        superJumpInput = false;
    }

    // explicitly classify 0 as different from positive/negative, since mathf.sign classifies 0 as positive
    float ternarySign (float x)
    {
        if (x > 0) return 1;
        else if (x < 0) return -1;
        else return 0;
    }

    bool isGrounded ()
    {
        return Physics2D.BoxCast(transform.position, GroundCheckBoxDimensions, 0, Vector2.down, GroundCheckFilter, groundedHitList, HalfHeight) != 0;
    }
}
