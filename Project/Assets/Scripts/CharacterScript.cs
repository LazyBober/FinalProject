using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] public float _health;
    [SerializeField] public float _maxHealth;
    [SerializeField] public float _damage;
    [SerializeField] public bool _overHealable;
    [SerializeField] public string _characterName;
    [SerializeField] public bool _canPushCart;
    [SerializeField] public bool _canBlockCart;
    [SerializeField] public string team;

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0f)
        {
            if (_health < 0f)
            {
                _health = 0f;
            }
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetStats(CharacterData data)
    {
        _health = data.Health;
        _maxHealth = data.MaxHealth;
        _damage = data.Damage;
        _overHealable = data.OverHealable;
        _characterName = data.CharacterName;
        _canPushCart = data.CanPushCart;
        _canBlockCart = data.CanBlockCart;
    }

    public virtual void Attack(CharacterScript enemy)
    {
        enemy._health -= _damage;
    }

    public void GetHeal(float amount)
    {
        if (_overHealable)
        {
            _health += amount;
        }
        else if (!_overHealable)
        {
            _health += amount;
            if (_health < _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    public void Special()
    {
        //tut specialni ataki yaki
        //vidbuvayutsia v zalezhnosti vid togo yakiy tse personazh
        Debug.Log("Attack example");
    }

    private void ArcherSpecial()
    {
        Color teamColor = transform.GetComponent<MeshRenderer>().material.color;
        CharacterScript[] charList = FindObjectsOfType<CharacterScript>();
        for (int i = 0; i < charList.Length; i++)
        {
            Color charColor = charList[i].GetComponent<MeshRenderer>().material.color;
            if (charColor != teamColor)
            {
                Attack(charList[i]);
            }
        }
    }
}
