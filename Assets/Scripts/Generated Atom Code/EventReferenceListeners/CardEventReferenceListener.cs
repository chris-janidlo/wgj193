using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Card`. Inherits from `AtomEventReferenceListener&lt;Card, CardEvent, CardEventReference, CardUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Card Event Reference Listener")]
    public sealed class CardEventReferenceListener : AtomEventReferenceListener<
        Card,
        CardEvent,
        CardEventReference,
        CardUnityEvent>
    { }
}
