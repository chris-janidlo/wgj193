using UnityEngine;
using UnityEngine.UI;
using crass;

public class EndBuildPhaseButtonHider : MonoBehaviour
{
    public TransitionableFloat FadeTransition;
    public Button Button;
    public CanvasGroup CanvasGroup;

    void Start ()
    {
        FadeTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        CanvasGroup.alpha = FadeTransition.Value;

    }

    public void OnCurrentPhaseChanged (Phase newPhase)
    {
        bool buildPhase = newPhase == Phase.Build;
        Button.interactable = buildPhase;
        FadeTransition.StartTransitionTo(buildPhase ? 1 : 0);
    }
}
