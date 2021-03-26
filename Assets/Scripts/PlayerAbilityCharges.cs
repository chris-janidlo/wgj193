using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Ability Charges", fileName = "newPlayerAbilityCharges.asset")]
public class PlayerAbilityCharges : ScriptableObject
{
    [SerializeField]
    int _dash, _glide, _superJump, _extraJump;

    public int Dash
    {
        get => _dash;
        set => _dash = Math.Max(value, 0);
    }

    public int Glide
    {
        get => _glide;
        set => _glide = Math.Max(value, 0);
    }

    public int SuperJump
    {
        get => _superJump;
        set => _superJump = Math.Max(value, 0);
    }

    public int ExtraJump
    {
        get => _extraJump;
        set => _extraJump = Math.Max(value, 0);
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
}
