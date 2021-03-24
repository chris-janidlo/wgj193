#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CardPair`. Inherits from `AtomDrawer&lt;CardPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CardPairEvent))]
    public class CardPairEventDrawer : AtomDrawer<CardPairEvent> { }
}
#endif
