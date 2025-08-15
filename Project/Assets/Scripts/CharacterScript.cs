using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private bool _overHealable;

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
        else if(!_overHealable)
        {
            _health += amount;
            if (_health < _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }
}
