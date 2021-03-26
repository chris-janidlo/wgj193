using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;
using TMPro;
using crass;

public class PlatformingAbilityMeter : MonoBehaviour
{
    public Ability Ability;
    public float RepeatedChangeDelay;

    public TextMeshProUGUI Label;
    public TransitionableFloat AlphaTransition;

    public LayoutGroup LayoutGroup;
    public PlatformingAbilityChargeVisual ChargeVisualPrefab;

    public PlayerAbilityCharges PlayerAbilityCharges;
    public Vector3Variable WorldPosition;

    List<PlatformingAbilityChargeVisual> instantiatedChargeVisuals;

    void Awake ()
    {
        PlayerAbilityCharges.NumberOfAbilityChargesDidChange += onAbilityChargesChanged;
        instantiatedChargeVisuals = new List<PlatformingAbilityChargeVisual>();

        AlphaTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        WorldPosition.Value = CameraCache.Main.ScreenToWorldPoint(transform.position);
        Label.alpha = AlphaTransition.Value;
    }

    void OnDestroy ()
    {
        // don't expect this to be destroyed, but just to be safe
        PlayerAbilityCharges.NumberOfAbilityChargesDidChange -= onAbilityChargesChanged;
    }

    public void OnPhaseChanged (Phase newPhase)
    {
        switch (newPhase)
        {
            case Phase.Build:
                AlphaTransition.FlashFromTo(1, 0);
                break;
            case Phase.Enemy:
                AlphaTransition.FlashFromTo(0, 1);
                break;
        }
    }

    void onAbilityChargesChanged (Ability ability, int oldValue, int newValue)
    {
        if (ability != Ability) return;

        StartCoroutine(meterAnimationRoutine(oldValue, newValue));
    }

    IEnumerator meterAnimationRoutine (int oldValue, int newValue)
    {
        int diff = newValue - oldValue;
        for (int i = 0; i < Math.Abs(diff); i++)
        {
            if (diff > 0)
            {
                var chargeVisual = Instantiate(ChargeVisualPrefab, LayoutGroup.transform);
                instantiatedChargeVisuals.Add(chargeVisual);
            }
            else
            {
                int popIndex = 0;
                instantiatedChargeVisuals[popIndex].Despawn();
                instantiatedChargeVisuals.RemoveAt(popIndex);
            }

            yield return new WaitForSeconds(RepeatedChangeDelay);
        }
    }
}
