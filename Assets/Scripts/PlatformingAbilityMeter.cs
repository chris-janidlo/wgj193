using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformingAbilityMeter : MonoBehaviour
{
    public Ability Ability;
    public float RepeatedChangeDelay;

    public LayoutGroup LayoutGroup;
    public PlatformingAbilityChargeVisual ChargeVisualPrefab;
    public PlayerAbilityCharges PlayerAbilityCharges;

    List<PlatformingAbilityChargeVisual> instantiatedChargeVisuals;

    void Awake ()
    {
        PlayerAbilityCharges.NumberOfAbilityChargesDidChange += onAbilityChargesChanged;
        instantiatedChargeVisuals = new List<PlatformingAbilityChargeVisual>();
    }

    void OnDestroy ()
    {
        // don't expect this to be destroyed, but just to be safe
        PlayerAbilityCharges.NumberOfAbilityChargesDidChange -= onAbilityChargesChanged;
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
