using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class FallingPlatform : MonoBehaviour
{
    public float Gravity, MaxFallSpeed;
    public float HalfHeight;
    public Vector2 GroundCheckBoxSize;
    public ContactFilter2D GroundCheckFilter;
    public float DeathTime;

    public TransitionableFloat SpawnSizeTransition;
    public Collider2D Collider;

    Vector2 originalPosition;
    float fallSpeed;
    bool falling, dead;
    float deathTimer;

    RaycastHit2D[] groundCheckHits;

    void Start ()
    {
        SpawnSizeTransition.AttachMonoBehaviour(this);
        SpawnSizeTransition.FlashFromTo(0, 1);
        groundCheckHits = new RaycastHit2D[1];
    }

    void Update ()
    {
        transform.localScale = Vector3.one * SpawnSizeTransition.Value;
        Collider.enabled = transform.localScale.x == 1;

        if (falling)
        {
            fallSpeed = Mathf.Min(fallSpeed + Gravity * Time.deltaTime, MaxFallSpeed);
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            if (Physics2D.BoxCast(transform.position, GroundCheckBoxSize, 0, Vector2.down, GroundCheckFilter, groundCheckHits, HalfHeight) != 0)
            {
                falling = false;
                dead = true;
                fallSpeed = 0;
                deathTimer = DeathTime;
                SpawnSizeTransition.FlashFromTo(1, 0);
            }
        }

        if (dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                dead = false;
                transform.position = originalPosition;
                SpawnSizeTransition.FlashFromTo(0, 1);
            }
        }
    }

    public void OnPlayerLanded ()
    {
        if (falling || dead) return;

        falling = true;
        originalPosition = transform.position;
    }
}
