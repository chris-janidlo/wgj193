using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `Card`. Inherits from `EquatableAtomVariable&lt;Card, CardPair, CardEvent, CardPairEvent, CardCardFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Card", fileName = "CardVariable")]
    public sealed class CardVariable : EquatableAtomVariable<Card, CardPair, CardEvent, CardPairEvent, CardCardFunction> { }
}
