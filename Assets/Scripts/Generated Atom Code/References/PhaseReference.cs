using System;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `Phase`. Inherits from `AtomReference&lt;Phase, PhasePair, PhaseConstant, PhaseVariable, PhaseEvent, PhasePairEvent, PhasePhaseFunction, PhaseVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class PhaseReference : AtomReference<
        Phase,
        PhasePair,
        PhaseConstant,
        PhaseVariable,
        PhaseEvent,
        PhasePairEvent,
        PhasePhaseFunction,
        PhaseVariableInstancer>, IEquatable<PhaseReference>
    {
        public PhaseReference() : base() { }
        public PhaseReference(Phase value) : base(value) { }
        public bool Equals(PhaseReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Phase other)
        {
            throw new NotImplementedException();
        } 
    }
}
