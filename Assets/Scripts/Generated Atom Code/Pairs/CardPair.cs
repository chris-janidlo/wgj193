using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Card&gt;`. Inherits from `IPair&lt;Card&gt;`.
    /// </summary>
    [Serializable]
    public struct CardPair : IPair<Card>
    {
        public Card Item1 { get => _item1; set => _item1 = value; }
        public Card Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Card _item1;
        [SerializeField]
        private Card _item2;

        public void Deconstruct(out Card item1, out Card item2) { item1 = Item1; item2 = Item2; }
    }
}