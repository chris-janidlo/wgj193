using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `PhasePair`. Inherits from `AtomEventInstancer&lt;PhasePair, PhasePairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/PhasePair Event Instancer")]
    public class PhasePairEventInstancer : AtomEventInstancer<PhasePair, PhasePairEvent> { }
}
