using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

public class PhaseText : MonoBehaviour
{
    public string BuildPhaseText, EnemyPhaseText, PlatformingPhaseText;
    public float PlatformingPhaseTextUptime;
    public TransitionableFloat TextFadeTransition;

    public TextMeshProUGUI TextContainer;

    void Start ()
    {
        TextFadeTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        TextContainer.alpha = TextFadeTransition.Value;
    }

    public void OnCurrentPhaseChanged (Phase newPhase)
    {
        switch (newPhase)
        {
            case Phase.Build:
                TextFadeTransition.FlashFromTo(0, 1);
                TextContainer.text = BuildPhaseText;
                break;

            case Phase.Enemy:
                StartCoroutine(fadeToText(EnemyPhaseText));
                break;

            case Phase.Platforming:
                StartCoroutine(platformingRoutine());
                break;
        }
    }

    IEnumerator fadeToText (string text)
    {
        TextFadeTransition.FlashFromTo(1, 0);
        yield return new WaitWhile(() => TextFadeTransition.Transitioning);
        TextContainer.text = text;
        TextFadeTransition.FlashFromTo(0, 1);
    }

    IEnumerator platformingRoutine ()
    {
        yield return fadeToText(PlatformingPhaseText);
        yield return new WaitForSeconds(PlatformingPhaseTextUptime);
        TextFadeTransition.FlashFromTo(1, 0);
    }
}
