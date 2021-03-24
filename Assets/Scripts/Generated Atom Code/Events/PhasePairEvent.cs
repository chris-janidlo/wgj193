using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `PhasePair`. Inherits from `AtomEvent&lt;PhasePair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/PhasePair", fileName = "PhasePairEvent")]
    public sealed class PhasePairEvent : AtomEvent<PhasePair> { }
}
