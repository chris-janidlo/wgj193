using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Card`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(CardVariable))]
    public sealed class CardVariableEditor : AtomVariableEditor<Card, CardPair> { }
}
