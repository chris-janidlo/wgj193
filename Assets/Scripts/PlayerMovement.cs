using System;
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
    public BasicMovementProfile GroundProfile, AirProfile, GlideProfile;

    // burst: initial instantaneous speed value when jumping
    // cut: when you release the jump button, your vertical speed is set to this if it's currently higher. essentially an air control value
    public float JumpSpeedBurst, JumpSpeedCut;
    public float EarlyJumpPressTime; // you can input jump this many seconds before landing and it will still count
    
    public float MinFallSpeedToStartGliding, MaxFallSpeedWhenGliding;

    public float DashSpeed;

    public float SuperJumpBurst, SpeedToEndSuperJumpState;

    public float HalfHeight;
    public Vector2 GroundCheckBoxDimensions; // should typically be set to x = player width plus walking over platform distance, y = vertical fudge
    public ContactFilter2D GroundCheckFilter;

    [Header("Animation")]
    public string HorizontalIntName;
    public string VerticalIntName, GroundedBoolName;

    [Header("References")]
    public Rigidbody2D Rigidbody;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public IntVariable ExtraJumpCharges, GlideCharges, DashCharges, SuperJumpCharges;

    Vector2 moveInput, moveInputMemory;
    bool jumpInput, dashInput, superJumpInput;
        
    bool grounded, gliding, jumping, superJumping;

    float earlyJumpPressTimer;

    // keep this at class-level to avoid making a new array every frame
    RaycastHit2D[] groundedHitList;

    void Start ()
    {
        groundedHitList = new RaycastHit2D[1];
        moveInputMemory = Vector2.right;
    }

    void Update ()
    {
        earlyJumpPressTimer = Mathf.Max(earlyJumpPressTimer - Time.deltaTime, 0);

        animate();
    }

    void FixedUpdate ()
    {
        platform();
    }

    public void OnMoveInput (CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero) moveInputMemory = moveInput;
    }

    public void OnJumpInput (CallbackContext context)
    {
        if (context.performed)
        {
            earlyJumpPressTimer = EarlyJumpPressTime;
        }

        jumpInput = context.ReadValueAsButton();
    }

    public void OnDashInput (CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            superJumpInput = context.performed;
        }
        else
        {
            // technically the action has successfully performed by here, but because it's a press it's already on to cancelled. since I manage the state of it later anyway, I'm just going to set it to true regardless here
            dashInput = true;
        }
    }

    void animate ()
    {
        SpriteRenderer.flipX = moveInputMemory.x < 0;

        Animator.SetInteger(HorizontalIntName, (int) ternarySign(Rigidbody.velocity.x));
        Animator.SetInteger(VerticalIntName, (int) ternarySign(Rigidbody.velocity.y));
        Animator.SetBool(GroundedBoolName, grounded);
    }

    void platform ()
    {
        grounded = isGrounded();

        fall();
        dash(); // dash goes after fall so that fall doesn't cancel an upward, grounded dash. it goes before jump so it can turn off jumping as needed
        superJump(); // super jump before jump since super jump overrides jump
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


        if (!gliding && GlideCharges.Value > 0 && Rigidbody.velocity.y < -MinFallSpeedToStartGliding && jumpInput)
        {
            gliding = true;
            GlideCharges.Value--;
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
        if (superJumping) return;

        float y = Rigidbody.velocity.y;

        if ((grounded || ExtraJumpCharges.Value > 0) && !jumping && earlyJumpPressTimer > 0)
        {
            // start of jump
            y = JumpSpeedBurst;
            jumping = true;
            earlyJumpPressTimer = 0;

            if (!grounded) ExtraJumpCharges.Value--;
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
            jumping = false;
        }

        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, y);
    }

    void move ()
    {
        float x = Rigidbody.velocity.x;
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

        Rigidbody.velocity = new Vector2(x, Rigidbody.velocity.y);
    }

    void dash ()
    {
        if (dashInput && DashCharges.Value > 0)
        {
            jumping = false;
            dashInput = false;
            DashCharges.Value--;

            Vector2 direction = moveInput;

            if (moveInput == Vector2.zero)
            {
                direction.x = Mathf.Sign(moveInputMemory.x);
            }

            Rigidbody.velocity = direction.normalized * DashSpeed;
        }
    }

    void superJump ()
    {
        if (grounded && superJumpInput && SuperJumpCharges.Value > 0)
        {
            superJumpInput = false;
            superJumping = true;
            SuperJumpCharges.Value--;

            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, SuperJumpBurst);
        }

        if (superJumping && !grounded && Rigidbody.velocity.y <= SpeedToEndSuperJumpState)
        {
            superJumping = false;
        }
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
