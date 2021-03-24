#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Phase`. Inherits from `AtomDrawer&lt;PhaseVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(PhaseVariable))]
    public class PhaseVariableDrawer : VariableDrawer<PhaseVariable> { }
}
#endif
