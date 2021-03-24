#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Phase`. Inherits from `AtomDrawer&lt;PhaseConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(PhaseConstant))]
    public class PhaseConstantDrawer : VariableDrawer<PhaseConstant> { }
}
#endif
