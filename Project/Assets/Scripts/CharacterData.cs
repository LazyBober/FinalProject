using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Stats")]
    public float Health;
    public float MaxHealth;
    public float Damage;
    public bool OverHealable;
    public string CharacterName;
    public bool CanPushCart;
    public bool CanBlockCart;
    public bool movable;

    [Header("Other")]
    public GameObject PrefabNormal;
    public GameObject PrefabBlue;
    public GameObject PrefabRed;
}
