using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Card`. Inherits from `AtomVariableInstancer&lt;CardVariable, CardPair, Card, CardEvent, CardPairEvent, CardCardFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Card Variable Instancer")]
    public class CardVariableInstancer : AtomVariableInstancer<
        CardVariable,
        CardPair,
        Card,
        CardEvent,
        CardPairEvent,
        CardCardFunction>
    { }
}
