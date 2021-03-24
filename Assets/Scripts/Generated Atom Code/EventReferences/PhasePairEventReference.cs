using System;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `PhasePair`. Inherits from `AtomEventReference&lt;PhasePair, PhaseVariable, PhasePairEvent, PhaseVariableInstancer, PhasePairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class PhasePairEventReference : AtomEventReference<
        PhasePair,
        PhaseVariable,
        PhasePairEvent,
        PhaseVariableInstancer,
        PhasePairEventInstancer>, IGetEvent 
    { }
}
