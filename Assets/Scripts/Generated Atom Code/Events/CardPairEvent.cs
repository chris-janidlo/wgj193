using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `CardPair`. Inherits from `AtomEvent&lt;CardPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CardPair", fileName = "CardPairEvent")]
    public sealed class CardPairEvent : AtomEvent<CardPair> { }
}
