using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityAtoms;
using TMPro;
using crass;

public class UICard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TransitionableVector2 ReturnToHandTransition;
    public TransitionableFloat ScaleTransition;

    public ContactFilter2D BuildZoneCheckFilter;

    public TextMeshProUGUI AbilityText;
    public Transform PlatformingBitsParent;

    bool interactible = true;

    Card card;
    Transform handParent, dragParent;
    CardBuildZone lockedBuildZone;

    Collider2D[] buildZoneCheckResults;
    Vector2 dragOffset;

    void Start ()
    {
        buildZoneCheckResults = new Collider2D[1];

        ReturnToHandTransition.AttachMonoBehaviour(this);
        ScaleTransition.AttachMonoBehaviour(this);

        ScaleTransition.FlashFromTo(0, 1);
    }

    void Update ()
    {
        transform.localScale = Vector3.one * Mathf.Max(ScaleTransition.Value, 0);
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        if (!interactible) return;

        dragOffset = (Vector2) transform.position - pointerWorldPosition(eventData);
        transform.SetParent(dragParent, true);
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (!interactible) return;
        transform.position = dragOffset + pointerWorldPosition(eventData);
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        if (!interactible) return;

        if (Physics2D.OverlapPoint(pointerWorldPosition(eventData), BuildZoneCheckFilter, buildZoneCheckResults) != 0)
        {
            var buildZone = buildZoneCheckResults[0].GetComponent<CardBuildZone>();

            if (!buildZone.HasCard)
            {
                lockToBuildZone(buildZone);
                return;
            }
        }

        StartCoroutine(sendBackToHand());
    }

    public void Initialize (Card card, Transform handParent, CardBuildZone initialBuildZone)
    {
        this.card = card;
        this.handParent = handParent;
        lockedBuildZone = initialBuildZone;

        dragParent = handParent.parent;

        var platformingBits = Instantiate(card.PlatformingBits, PlatformingBitsParent);
        platformingBits.AlwaysDisableColliders = true;
        AbilityText.text = card.Ability.ToString();
    }

    public void OnCurrentPhaseChanged (Phase newPhase)
    {
        if (newPhase != Phase.Enemy) return;

        interactible = false;

        if (lockedBuildZone != null)
        {
            StartCoroutine(buildCard());
        }
        else
        {
            StartCoroutine(sendCardToPlatformingUI());
        }
    }

    Vector2 pointerWorldPosition (PointerEventData eventData)
    {
        return CameraCache.Main.ScreenToWorldPoint(eventData.position);
    }

    void lockToBuildZone (CardBuildZone buildZone)
    {
        if (lockedBuildZone != null) lockedBuildZone.UnsetCard();

        lockedBuildZone = buildZone;
        lockedBuildZone.SetCard(card);

        transform.position = buildZone.transform.position;
    }

    IEnumerator sendBackToHand ()
    {
        interactible = false;

        if (lockedBuildZone != null)
        {
            lockedBuildZone.UnsetCard();
            lockedBuildZone = null;
        }

        Vector2 origPos = transform.position;
        transform.SetParent(handParent, false);
        yield return null;
        Vector2 targetPos = transform.position;

        ReturnToHandTransition.FlashFromTo(origPos, targetPos);

        while (ReturnToHandTransition.Transitioning)
        {
            transform.position = ReturnToHandTransition.Value;
            yield return null;
        }

        interactible = true;
    }

    IEnumerator buildCard ()
    {
        ScaleTransition.FlashFromTo(1, 0);
        yield return new WaitWhile(() => ScaleTransition.Transitioning);

        Destroy(gameObject);
        lockedBuildZone.Build();
    }

    IEnumerator sendCardToPlatformingUI ()
    {
        throw new System.NotImplementedException();
    }
}
