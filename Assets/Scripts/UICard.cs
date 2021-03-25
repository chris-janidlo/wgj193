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
    public TransitionableVector2 DummyFollowTransition;
    public TransitionableFloat ScaleTransition;

    public ContactFilter2D BuildZoneCheckFilter;

    public TextMeshProUGUI AbilityText;
    public Transform PlatformingBitsParent, DummyLayoutElement;

    bool dragging;
    bool interactible = true;

    Card card;
    Transform handParent, dragParent;
    CardBuildZone lockedBuildZone;

    Collider2D[] buildZoneCheckResults;
    Vector2 dragOffset;

    void Awake ()
    {
        buildZoneCheckResults = new Collider2D[1];

        DummyFollowTransition.AttachMonoBehaviour(this);
        ScaleTransition.AttachMonoBehaviour(this);

        ScaleTransition.Value = 0;
    }

    void Update ()
    {
        transform.localScale = Vector3.one * Mathf.Max(ScaleTransition.Value, 0);

        if (dragging || transform.position == DummyLayoutElement.position)
        {
            DummyFollowTransition.Value = DummyLayoutElement.position;
        }
        else
        {
            DummyFollowTransition.StartTransitionToIfNotAlreadyStarted(DummyLayoutElement.position);
        }

        transform.position = DummyFollowTransition.Value;
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        if (!interactible || DummyFollowTransition.Transitioning) return;

        dragging = true;

        dragOffset = (Vector2) DummyLayoutElement.position - pointerWorldPosition(eventData);
        DummyLayoutElement.SetParent(dragParent, true);
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (!interactible || DummyFollowTransition.Transitioning) return;

        DummyLayoutElement.position = dragOffset + pointerWorldPosition(eventData);
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        if (!interactible || DummyFollowTransition.Transitioning) return;

        dragging = false;

        if (Physics2D.OverlapPoint(pointerWorldPosition(eventData), BuildZoneCheckFilter, buildZoneCheckResults) != 0)
        {
            var buildZone = buildZoneCheckResults[0].GetComponent<CardBuildZone>();

            if (!buildZone.HasCard)
            {
                lockToBuildZone(buildZone);
                return;
            }
        }

        sendBackToHand();
    }

    public void Initialize (Card card, Transform handParent, CardBuildZone initialBuildZone)
    {
        this.card = card;
        this.handParent = handParent;
        lockedBuildZone = initialBuildZone;

        var platformingBits = Instantiate(card.PlatformingBits, PlatformingBitsParent);
        platformingBits.AlwaysDisableColliders = true;
        AbilityText.text = card.Ability.ToString();

        dragParent = handParent.parent;

        transform.SetParent(dragParent, true);
        DummyLayoutElement.SetParent(handParent, false);

        ScaleTransition.FlashFromTo(0, 1);
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

        DummyLayoutElement.position = buildZone.transform.position;
    }

    void sendBackToHand ()
    {
        if (lockedBuildZone != null)
        {
            lockedBuildZone.UnsetCard();
            lockedBuildZone = null;
        }

        DummyLayoutElement.SetParent(handParent, false);
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
        Destroy(gameObject);
        yield return null;
    }
}
