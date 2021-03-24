using System;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `Card`. Inherits from `AtomEventReference&lt;Card, CardVariable, CardEvent, CardVariableInstancer, CardEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CardEventReference : AtomEventReference<
        Card,
        CardVariable,
        CardEvent,
        CardVariableInstancer,
        CardEventInstancer>, IGetEvent 
    { }
}
