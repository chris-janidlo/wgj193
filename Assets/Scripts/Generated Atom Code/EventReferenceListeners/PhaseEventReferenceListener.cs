using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Phase`. Inherits from `AtomEventReferenceListener&lt;Phase, PhaseEvent, PhaseEventReference, PhaseUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Phase Event Reference Listener")]
    public sealed class PhaseEventReferenceListener : AtomEventReferenceListener<
        Phase,
        PhaseEvent,
        PhaseEventReference,
        PhaseUnityEvent>
    { }
}
