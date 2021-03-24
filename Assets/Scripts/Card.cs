using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

[Serializable]
public class Card : IEquatable<Card>
{
    public CardPlatformingBits PlatformingBits;
    public Ability Ability;

    public bool Equals (Card other)
    {
        return this == other;
    }
}
