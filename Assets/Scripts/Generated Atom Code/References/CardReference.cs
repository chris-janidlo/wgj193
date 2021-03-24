using System;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `Card`. Inherits from `EquatableAtomReference&lt;Card, CardPair, CardConstant, CardVariable, CardEvent, CardPairEvent, CardCardFunction, CardVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CardReference : EquatableAtomReference<
        Card,
        CardPair,
        CardConstant,
        CardVariable,
        CardEvent,
        CardPairEvent,
        CardCardFunction,
        CardVariableInstancer>, IEquatable<CardReference>
    {
        public CardReference() : base() { }
        public CardReference(Card value) : base(value) { }
        public bool Equals(CardReference other) { return base.Equals(other); }
    }
}
