using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Stats")]
    public float Health;
    public float MaxHealth;
    public float Damage;
    public bool OverHealable;
}
