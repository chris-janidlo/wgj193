using UnityEngine;
using crass;

public class EndBuildPhaseButtonHider : MonoBehaviour
{
    public TransitionableFloat FadeTransition;
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
        FadeTransition.StartTransitionTo(newPhase == Phase.Build ? 1 : 0);
    }
}
