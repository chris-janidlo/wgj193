using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Phase&gt;`. Inherits from `IPair&lt;Phase&gt;`.
    /// </summary>
    [Serializable]
    public struct PhasePair : IPair<Phase>
    {
        public Phase Item1 { get => _item1; set => _item1 = value; }
        public Phase Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Phase _item1;
        [SerializeField]
        private Phase _item2;

        public void Deconstruct(out Phase item1, out Phase item2) { item1 = Item1; item2 = Item2; }
    }
}