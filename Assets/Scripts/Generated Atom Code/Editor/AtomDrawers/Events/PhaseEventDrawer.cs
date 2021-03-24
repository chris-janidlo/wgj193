#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Phase`. Inherits from `AtomDrawer&lt;PhaseEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(PhaseEvent))]
    public class PhaseEventDrawer : AtomDrawer<PhaseEvent> { }
}
#endif
