#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CardPair`. Inherits from `AtomEventEditor&lt;CardPair, CardPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CardPairEvent))]
    public sealed class CardPairEventEditor : AtomEventEditor<CardPair, CardPairEvent> { }
}
#endif
