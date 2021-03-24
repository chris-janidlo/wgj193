using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Phase`. Inherits from `SetVariableValue&lt;Phase, PhasePair, PhaseVariable, PhaseConstant, PhaseReference, PhaseEvent, PhasePairEvent, PhaseVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Phase", fileName = "SetPhaseVariableValue")]
    public sealed class SetPhaseVariableValue : SetVariableValue<
        Phase,
        PhasePair,
        PhaseVariable,
        PhaseConstant,
        PhaseReference,
        PhaseEvent,
        PhasePairEvent,
        PhasePhaseFunction,
        PhaseVariableInstancer>
    { }
}
