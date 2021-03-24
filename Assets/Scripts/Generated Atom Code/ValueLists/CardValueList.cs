using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Card`. Inherits from `AtomValueList&lt;Card, CardEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Card", fileName = "CardValueList")]
    public sealed class CardValueList : AtomValueList<Card, CardEvent> { }
}
