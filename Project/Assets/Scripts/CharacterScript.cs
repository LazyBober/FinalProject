using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float damage;
    [SerializeField] public bool overHealable;
    [SerializeField] public string characterName;
    [SerializeField] public bool canPushCart;
    [SerializeField] public bool canBlockCart;
    [SerializeField] public string team;
    [SerializeField] public bool movable;

    private bool stunned;

    public bool inEnemySMRange;

    private void Awake()
    {
        stunned = false;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if (health < 0f)
            {
                health = 0f;
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
        health = data.Health;
        maxHealth = data.MaxHealth;
        damage = data.Damage;
        overHealable = data.OverHealable;
        characterName = data.CharacterName;
        canPushCart = data.CanPushCart;
        canBlockCart = data.CanBlockCart;
        movable = data.movable;
    }

    public virtual void Attack(CharacterScript enemy)
    {
        enemy.health -= damage;
    }

    public void GetHeal(float amount)
    {
        if (overHealable)
        {
            health += amount;
        }
        else if (!overHealable)
        {
            health += amount;
            if (health < maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public void Special()
    {
        if (!stunned)
        {
            if (name == "archer")
            {
                ArcherSpecial();
            }
        }
    }

    private void ArcherSpecial()
    {
        Color teamColor = transform.GetComponent<MeshRenderer>().material.color;
        CharacterScript[] charList = FindObjectsOfType<CharacterScript>();
        for (int i = 0; i < charList.Length; i++)
        {
            if (charList[i].team != team)
            {
                Attack(charList[i]);
            }
        }
    }

    private void StrongManSpecial()
    {
        CharacterScript[] charList = FindObjectsOfType<CharacterScript>();
        foreach (CharacterScript character in charList)
        {
            if (character.team != team && character.inEnemySMRange)
            {
                Attack(character);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (name == "juggernaut")
        {
            CharacterScript character = collision.GetComponent<CharacterScript>();
            if (character != null)
            {
                character.inEnemySMRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
