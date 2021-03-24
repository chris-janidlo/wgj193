using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `Card`. Inherits from `SetVariableValue&lt;Card, CardPair, CardVariable, CardConstant, CardReference, CardEvent, CardPairEvent, CardVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Card", fileName = "SetCardVariableValue")]
    public sealed class SetCardVariableValue : SetVariableValue<
        Card,
        CardPair,
        CardVariable,
        CardConstant,
        CardReference,
        CardEvent,
        CardPairEvent,
        CardCardFunction,
        CardVariableInstancer>
    { }
}
