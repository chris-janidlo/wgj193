#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Card`. Inherits from `AtomDrawer&lt;CardEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CardEvent))]
    public class CardEventDrawer : AtomDrawer<CardEvent> { }
}
#endif
