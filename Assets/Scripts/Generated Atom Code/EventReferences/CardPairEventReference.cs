using System;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `CardPair`. Inherits from `AtomEventReference&lt;CardPair, CardVariable, CardPairEvent, CardVariableInstancer, CardPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CardPairEventReference : AtomEventReference<
        CardPair,
        CardVariable,
        CardPairEvent,
        CardVariableInstancer,
        CardPairEventInstancer>, IGetEvent 
    { }
}
