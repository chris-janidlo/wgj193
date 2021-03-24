using System;
using UnityEngine.Events;

namespace UnityAtoms
{
    /// <summary>
    /// None generic Unity Event of type `Card`. Inherits from `UnityEvent&lt;Card&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CardUnityEvent : UnityEvent<Card> { }
}
