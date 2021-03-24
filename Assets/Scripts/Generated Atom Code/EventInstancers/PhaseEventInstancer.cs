using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Phase`. Inherits from `AtomEventInstancer&lt;Phase, PhaseEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Phase Event Instancer")]
    public class PhaseEventInstancer : AtomEventInstancer<Phase, PhaseEvent> { }
}
