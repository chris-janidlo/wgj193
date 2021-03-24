using UnityEngine;
using System;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `Phase`. Inherits from `AtomVariable&lt;Phase, PhasePair, PhaseEvent, PhasePairEvent, PhasePhaseFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Phase", fileName = "PhaseVariable")]
    public sealed class PhaseVariable : AtomVariable<Phase, PhasePair, PhaseEvent, PhasePairEvent, PhasePhaseFunction>
    {
        protected override bool ValueEquals(Phase other)
        {
            return Value == other;
        }
    }
}
