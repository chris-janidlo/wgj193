using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Card`. Inherits from `AtomEventInstancer&lt;Card, CardEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Card Event Instancer")]
    public class CardEventInstancer : AtomEventInstancer<Card, CardEvent> { }
}
