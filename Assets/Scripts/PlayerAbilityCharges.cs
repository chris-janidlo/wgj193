using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Ability Charges", fileName = "newPlayerAbilityCharges.asset")]
public class PlayerAbilityCharges : ScriptableObject
{
    public delegate void NumberOfAbilityChargesDidChangeDelegate (Ability ability, int oldValue, int newValue);
    public event NumberOfAbilityChargesDidChangeDelegate NumberOfAbilityChargesDidChange;

    public bool InfiniteAbilityUse;

    [SerializeField]
    int _dash, _glide, _superJump, _extraJump;

    public int Dash
    {
        get => getAbilityCharges(_dash);
        set => setAbilityCharges(ref _dash, value, Ability.Dash);
    }

    public int Glide
    {
        get => getAbilityCharges(_glide);
        set => setAbilityCharges(ref _glide, value, Ability.Glide);
    }

    public int SuperJump
    {
        get => getAbilityCharges(_superJump);
        set => setAbilityCharges(ref _superJump, value, Ability.SuperJump);
    }

    public int ExtraJump
    {
        get => getAbilityCharges(_extraJump);
        set => setAbilityCharges(ref _extraJump, value, Ability.ExtraJump);
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

    public void Initialize ()
    {
        InfiniteAbilityUse = false;

        _dash = 0;
        _glide = 0;
        _extraJump = 0;
        _superJump = 0;
    }

    public void Clear ()
    {
        Dash = 0;
        Glide = 0;
        ExtraJump = 0;
        SuperJump = 0;
    }

    int getAbilityCharges (int charges)
    {
        return InfiniteAbilityUse ? 1 : charges;
    }

    void setAbilityCharges (ref int charges, int newValue, Ability ability)
    {
        if (InfiniteAbilityUse) return;

        int zeroedValue = Math.Max(newValue, 0);
        if (charges == zeroedValue) return;

        int oldValue = charges;
        charges = zeroedValue;

        NumberOfAbilityChargesDidChange?.Invoke(ability, oldValue, charges);
    }
}
