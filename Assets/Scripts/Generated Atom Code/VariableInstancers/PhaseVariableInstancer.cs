using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Phase`. Inherits from `AtomVariableInstancer&lt;PhaseVariable, PhasePair, Phase, PhaseEvent, PhasePairEvent, PhasePhaseFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Phase Variable Instancer")]
    public class PhaseVariableInstancer : AtomVariableInstancer<
        PhaseVariable,
        PhasePair,
        Phase,
        PhaseEvent,
        PhasePairEvent,
        PhasePhaseFunction>
    { }
}
