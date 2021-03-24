#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `PhasePair`. Inherits from `AtomEventEditor&lt;PhasePair, PhasePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(PhasePairEvent))]
    public sealed class PhasePairEventEditor : AtomEventEditor<PhasePair, PhasePairEvent> { }
}
#endif
