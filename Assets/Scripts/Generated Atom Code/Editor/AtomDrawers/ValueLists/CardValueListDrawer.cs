#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Card`. Inherits from `AtomDrawer&lt;CardValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CardValueList))]
    public class CardValueListDrawer : AtomDrawer<CardValueList> { }
}
#endif
