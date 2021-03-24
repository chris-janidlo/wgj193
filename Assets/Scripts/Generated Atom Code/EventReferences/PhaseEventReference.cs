using System;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Phase`. Inherits from `AtomEventReference&lt;Phase, PhaseVariable, PhaseEvent, PhaseVariableInstancer, PhaseEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class PhaseEventReference : AtomEventReference<
        Phase,
        PhaseVariable,
        PhaseEvent,
        PhaseVariableInstancer,
        PhaseEventInstancer>, IGetEvent 
    { }
}
