using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `PhasePair`. Inherits from `AtomEventReferenceListener&lt;PhasePair, PhasePairEvent, PhasePairEventReference, PhasePairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/PhasePair Event Reference Listener")]
    public sealed class PhasePairEventReferenceListener : AtomEventReferenceListener<
        PhasePair,
        PhasePairEvent,
        PhasePairEventReference,
        PhasePairUnityEvent>
    { }
}
