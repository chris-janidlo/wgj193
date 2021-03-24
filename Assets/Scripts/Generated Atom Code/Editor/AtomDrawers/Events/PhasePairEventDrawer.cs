#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `PhasePair`. Inherits from `AtomDrawer&lt;PhasePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(PhasePairEvent))]
    public class PhasePairEventDrawer : AtomDrawer<PhasePairEvent> { }
}
#endif
