using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Phase`. Inherits from `AtomValueList&lt;Phase, PhaseEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Phase", fileName = "PhaseValueList")]
    public sealed class PhaseValueList : AtomValueList<Phase, PhaseEvent> { }
}
