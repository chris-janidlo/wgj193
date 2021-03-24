#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Phase`. Inherits from `AtomDrawer&lt;PhaseValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(PhaseValueList))]
    public class PhaseValueListDrawer : AtomDrawer<PhaseValueList> { }
}
#endif
