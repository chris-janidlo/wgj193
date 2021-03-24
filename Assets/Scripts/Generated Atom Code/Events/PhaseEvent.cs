using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Phase`. Inherits from `AtomEvent&lt;Phase&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Phase", fileName = "PhaseEvent")]
    public sealed class PhaseEvent : AtomEvent<Phase> { }
}
