using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth;
    public LayerMask EnemyLayers;

    public IntVariable CurrentPlayerHealth;
    public PlayerLifeCycleManager PlayerLifeCycleManager;

    void Start ()
    {
        CurrentPlayerHealth.Value = MaxHealth;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        // if the colliding gameobject is in the enemy layermask. see http://answers.unity.com/answers/1137700/view.html
        if (EnemyLayers == (EnemyLayers | (1 << collision.gameObject.layer)))
        {
            CurrentPlayerHealth.Value--;
            
            if (CurrentPlayerHealth.Value <= 0)
            {
                PlayerLifeCycleManager.Die();
            }
        }
    }
}
