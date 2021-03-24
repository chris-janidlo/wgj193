#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Phase`. Inherits from `AtomEventEditor&lt;Phase, PhaseEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(PhaseEvent))]
    public sealed class PhaseEventEditor : AtomEventEditor<Phase, PhaseEvent> { }
}
#endif
