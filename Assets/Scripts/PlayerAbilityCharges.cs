using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Ability Charges", fileName = "newPlayerAbilityCharges.asset")]
public class PlayerAbilityCharges : ScriptableObject
{
    public delegate void NumberOfAbilityChargesDidChangeDelegate (Ability ability, int oldValue, int newValue);
    public event NumberOfAbilityChargesDidChangeDelegate NumberOfAbilityChargesDidChange;

    [SerializeField]
    int _dash, _glide, _superJump, _extraJump;

    public int Dash
    {
        get => _dash;
        set => changeAbilityCharges(ref _dash, value, Ability.Dash);
    }

    public int Glide
    {
        get => _glide;
        set => changeAbilityCharges(ref _glide, value, Ability.Glide);
    }

    public int SuperJump
    {
        get => _superJump;
        set => changeAbilityCharges(ref _superJump, value, Ability.SuperJump);
    }

    public int ExtraJump
    {
        get => _extraJump;
        set => changeAbilityCharges(ref _extraJump, value, Ability.ExtraJump);
    }

    public int this[Ability ability]
    {
        get
        {
            switch (ability)
            {
                case Ability.Dash:
                    return Dash;
                case Ability.Glide:
                    return Glide;
                case Ability.SuperJump:
                    return SuperJump;
                case Ability.ExtraJump:
                    return ExtraJump;

                default:
                    throw new ArgumentException($"unexpected Ability value {ability}");
            }
        }

        set
        {
            switch (ability)
            {
                case Ability.Dash:
                    Dash = value;
                    break;
                case Ability.Glide:
                    Glide = value;
                    break;
                case Ability.SuperJump:
                    SuperJump = value;
                    break;
                case Ability.ExtraJump:
                    ExtraJump = value;
                    break;

                default:
                    throw new ArgumentException($"unexpected Ability value {ability}");
            }
        }
    }

    void changeAbilityCharges (ref int charges, int newValue, Ability ability)
    {
        int zeroedValue = Math.Max(newValue, 0);
        if (charges == zeroedValue) return;

        int oldValue = charges;
        charges = zeroedValue;

        NumberOfAbilityChargesDidChange?.Invoke(ability, oldValue, charges);
    }
}
