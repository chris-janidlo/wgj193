#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Card`. Inherits from `AtomEventEditor&lt;Card, CardEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CardEvent))]
    public sealed class CardEventEditor : AtomEventEditor<Card, CardEvent> { }
}
#endif
