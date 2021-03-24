using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Phase`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(PhaseVariable))]
    public sealed class PhaseVariableEditor : AtomVariableEditor<Phase, PhasePair> { }
}
