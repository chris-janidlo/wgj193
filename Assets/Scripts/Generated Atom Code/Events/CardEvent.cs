using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Card`. Inherits from `AtomEvent&lt;Card&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Card", fileName = "CardEvent")]
    public sealed class CardEvent : AtomEvent<Card> { }
}
