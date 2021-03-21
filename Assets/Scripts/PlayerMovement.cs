using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
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
    public BasicMovementProfile GroundProfile, AirProfile, GlideProfile;

    // burst: initial instantaneous speed value when jumping
    // cut: when you release the jump button, your vertical speed is set to this if it's currently higher. essentially an air control value
    public float JumpSpeedBurst, JumpSpeedCut;
    public float EarlyJumpPressTime; // you can input jump this many seconds before landing and it will still count
    
    public float MinFallSpeedToStartGliding, MaxFallSpeedWhenGliding;

    public float HalfHeight;
    public Vector2 GroundCheckBoxDimensions; // should typically be set to x = player width plus walking over platform distance, y = vertical fudge
    public ContactFilter2D GroundCheckFilter;

    [Header("Ability Charges")]
    public int GlideCharges;

    [Header("References")]
    public Rigidbody2D Rigidbody;

    float moveInput;
    bool jumpInput;
        
    bool grounded, gliding, jumping;

    float earlyJumpPressTimer;

    // keep this at class-level to avoid making a new array every frame
    RaycastHit2D[] groundedHitList;

    void Start ()
    {
        groundedHitList = new RaycastHit2D[1];
    }

    void Update ()
    {
        earlyJumpPressTimer = Math.Max(earlyJumpPressTimer - Time.deltaTime, 0);
    }

    void FixedUpdate ()
    {
        platform();
    }

    public void OnMoveInput (CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    public void OnJumpInput (CallbackContext context)
    {
        if (context.performed)
        {
            earlyJumpPressTimer = EarlyJumpPressTime;
        }

        jumpInput = context.ReadValueAsButton();
    }

    void platform ()
    {
        grounded = isGrounded();

        fall();
        jump();
        move();
    }

    void fall ()
    {
        if (grounded)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
            return;
        }

        Rigidbody.velocity += Vector2.down * Gravity * Time.deltaTime;


        if (!gliding && GlideCharges > 0 && Rigidbody.velocity.y < -MinFallSpeedToStartGliding && jumpInput)
        {
            gliding = true;
            GlideCharges--;
        }

        if (gliding)
        {
            Rigidbody.velocity = new Vector2
            (
                Rigidbody.velocity.x,
                Mathf.Max(Rigidbody.velocity.y, -MaxFallSpeedWhenGliding)
            );

        }

        if (!jumpInput || grounded) gliding = false;
    }

    void jump ()
    {
        float y = Rigidbody.velocity.y;

        if (grounded && !jumping && earlyJumpPressTimer > 0)
        {
            // start of jump
            y = JumpSpeedBurst;
            jumping = true;
        }
        else if (grounded && jumping && y <= 0)
        {
            // landing
            jumping = false;
        }
        else if (jumping && !jumpInput && y > JumpSpeedCut)
        {
            // letting go of jump
            y = JumpSpeedCut;
        }

        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, y);
    }

    void move ()
    {
        float x = Rigidbody.velocity.x;
        BasicMovementProfile profile;

        if (gliding) profile = GlideProfile;
        else if (!grounded) profile = AirProfile;
        else profile = GroundProfile;

        if (moveInput == 0)
        {
            Vector2 deceleration = Mathf.Sign(x) * Vector2.right * profile.Deceleration * Time.deltaTime;
            deceleration = Vector2.ClampMagnitude(deceleration, Mathf.Abs(x));
            x -= deceleration.x;
        }
        else
        {
            float acceleration = profile.Acceleration;

            if (x != 0 && ternarySign(moveInput) != ternarySign(x))
            {
                // if we're switching directions, and the deceleration is faster, use the deceleration
                acceleration = Mathf.Max(profile.Acceleration, profile.Deceleration);
            }

            x = Mathf.Clamp
            (
                x + moveInput * acceleration * Time.deltaTime,
                -profile.MaxSpeed,
                profile.MaxSpeed
            );
        }

        Rigidbody.velocity = new Vector2(x, Rigidbody.velocity.y);
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
