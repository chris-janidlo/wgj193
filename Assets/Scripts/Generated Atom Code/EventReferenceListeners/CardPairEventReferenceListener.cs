using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `CardPair`. Inherits from `AtomEventReferenceListener&lt;CardPair, CardPairEvent, CardPairEventReference, CardPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/CardPair Event Reference Listener")]
    public sealed class CardPairEventReferenceListener : AtomEventReferenceListener<
        CardPair,
        CardPairEvent,
        CardPairEventReference,
        CardPairUnityEvent>
    { }
}
