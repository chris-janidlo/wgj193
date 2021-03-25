using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;
using crass;

public class CardBuildZone : MonoBehaviour
{
    public bool HasCard => CurrentCard != null;

    public Card CurrentCard { get; private set; }

    [Tooltip("The floor this build zone is in")]
    public int Floor;
    public IntVariable PlayerFloor;

    public TransitionableFloat PlatformingBitsSizeTransition;

    public Transform PlatformingBitsParent;
    public LayoutGroup HandLayoutGroup;
    public UICard UICardPrefab;

    CardPlatformingBits currentlyInstantiatedPlatformingBits;

    void Start ()
    {
        PlatformingBitsSizeTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        PlatformingBitsParent.localScale = Vector3.one * Mathf.Max(PlatformingBitsSizeTransition.Value, 0);
    }

    // assumes PlayerFloor has been updated before this is called
    public void OnCurrentPhaseChanged (Phase newPhase)
    {
        if (!HasCard) return;

        if (newPhase != Phase.Build) return;

        if (Floor > PlayerFloor.Value)
        {
            // game manager should retrieve the card from this and put it in the discard pile
            StartCoroutine(discardAnimation());
        }
        else if (Floor == PlayerFloor.Value)
        {
            Instantiate(UICardPrefab, HandLayoutGroup.transform.parent).Initialize(CurrentCard, HandLayoutGroup.transform, this);
        }
        else
        {
            // do nothing - this is a card on a floor that's below the player. the player isn't allowed to interact with it, so no new UICard, and they don't get these cards back either, so no death animation
        }
    }

    public void SetCard (Card card)
    {
        if (HasCard)
        {
            throw new InvalidOperationException("can't set a card to a build zone that already has one");
        }

        CurrentCard = card;
    }

    public void UnsetCard ()
    {
        if (!HasCard)
        {
            throw new InvalidOperationException("can't remove a card from a build zone that doesn't have one");
        }

        CurrentCard = null;
    }

    public void Build ()
    {
        if (!HasCard)
        {
            throw new InvalidOperationException("can't build a build zone that doesn't have a card");
        }

        currentlyInstantiatedPlatformingBits = Instantiate(CurrentCard.PlatformingBits, PlatformingBitsParent);
        PlatformingBitsSizeTransition.StartTransitionTo(1f / 3f);
    }

    IEnumerator discardAnimation ()
    {
        PlatformingBitsSizeTransition.StartTransitionTo(0);

        yield return new WaitWhile(() => PlatformingBitsSizeTransition.Transitioning);

        Destroy(currentlyInstantiatedPlatformingBits.gameObject);
        currentlyInstantiatedPlatformingBits = null;
        CurrentCard = null;
    }
}
