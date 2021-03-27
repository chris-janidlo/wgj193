using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityAtoms.BaseAtoms;
using TMPro;
using crass;

public class UICard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TransitionableVector2 DummyFollowTransition;
    public TransitionableFloat ScaleTransition;

    public ContactFilter2D BuildZoneCheckFilter;

    public TextMeshProUGUI AbilityText;
    public Transform PlatformingBitsParent, DummyLayoutElement;

    public PlayerAbilityCharges PlayerAbilityCharges;
    public Vector3Variable DashMeterLocation, GlideMeterLocation, ExtraJumpMeterLocation, SuperJumpMeterLocation;

    public TransitionableFloat ToPlatformingUITransition;
    [Range(0, 1)]
    public float ToPlatformingUITargetScale;

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
        ToPlatformingUITransition.AttachMonoBehaviour(this);

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

        transform.SetAsLastSibling();
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

        instantiatePlatformingBits();
        AbilityText.text = card.Ability.ToString();

        dragParent = handParent.parent;

        transform.SetParent(dragParent, true);

        if (initialBuildZone != null)
        {
            DummyLayoutElement.SetParent(dragParent, true);
            DummyLayoutElement.position = initialBuildZone.transform.position;
        }
        else
        {
            DummyLayoutElement.SetParent(handParent, false);
        }

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

    void instantiatePlatformingBits ()
    {
        var platformingBits = Instantiate(card.PlatformingBits, PlatformingBitsParent);
        platformingBits.AlwaysDisableColliders = true;

        // super duper ugly hack: in order to prevent SpriteRenderers from drawing over cards they shouldn't, just replace them with UI Image components. you know what they say, if it's stupid but it works...
        foreach (var spriteRenderer in platformingBits.GetComponentsInChildren<SpriteRenderer>())
        {
            var sprite = spriteRenderer.sprite;
            spriteRenderer.gameObject.AddComponent<Image>().sprite = sprite;
            Destroy(spriteRenderer);
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

        destroyCard();
        lockedBuildZone.Build();
    }

    IEnumerator sendCardToPlatformingUI ()
    {
        Vector3 originalPosition = transform.position;
        float originalScale = transform.localScale.x;

        Vector3Variable targetPosition = null;

        switch (card.Ability)
        {
            case Ability.Dash:
                targetPosition = DashMeterLocation;
                break;

            case Ability.Glide:
                targetPosition = GlideMeterLocation;
                break;

            case Ability.ExtraJump:
                targetPosition = ExtraJumpMeterLocation;
                break;

            case Ability.SuperJump:
                targetPosition = SuperJumpMeterLocation;
                break;
        }

        ToPlatformingUITransition.FlashFromTo(0, 1);

        while (ToPlatformingUITransition.Transitioning)
        {
            float t = ToPlatformingUITransition.Value;

            transform.position = Vector3.Lerp(originalPosition, targetPosition.Value, t);
            transform.localScale = Vector3.one * Mathf.Lerp(originalScale, ToPlatformingUITargetScale, t);
            
            yield return null;
        }

        PlayerAbilityCharges[card.Ability]++;
        destroyCard();
    }

    void destroyCard ()
    {
        Destroy(gameObject);
        Destroy(DummyLayoutElement.gameObject);
    }
}
