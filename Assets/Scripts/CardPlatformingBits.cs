using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlatformingBits : MonoBehaviour
{
    [SerializeField]
    bool _alwaysDisableColliders;
    public bool AlwaysDisableColliders
    {
        get => _alwaysDisableColliders;
        set
        {
            _alwaysDisableColliders = value;
            if (!value) setColliderState(false);
        }
    }

    public List<Collider2D> Colliders;

    [Tooltip("This isn't used anywhere in the code - just notes for me")]
    [TextArea(5, 500)]
    [SerializeField]
    string _designNotes;

    void Start ()
    {
        if (AlwaysDisableColliders) setColliderState(false);
    }

    public void OnCurrentPhaseChanged (Phase newPhase)
    {
        setColliderState(!AlwaysDisableColliders && newPhase == Phase.Platforming);
    }

    void setColliderState (bool value)
    {
        Colliders.ForEach(c => c.enabled = value);
    }
}
