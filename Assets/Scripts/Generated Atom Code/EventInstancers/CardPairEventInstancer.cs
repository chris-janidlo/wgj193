using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `CardPair`. Inherits from `AtomEventInstancer&lt;CardPair, CardPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/CardPair Event Instancer")]
    public class CardPairEventInstancer : AtomEventInstancer<CardPair, CardPairEvent> { }
}
