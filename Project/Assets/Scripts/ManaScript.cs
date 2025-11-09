using TMPro;
using UnityEngine;

public class ManaScript : MonoBehaviour
{
    public int currentMana = 0;
    [SerializeField] private int maxMana = 10;
    [SerializeField] private float giveManaInterval = 2f;

    [SerializeField] private TextMeshProUGUI text;

    public void GiveMana()
    {
        if (currentMana < maxMana)
        {
            currentMana++;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        text.text = $"mana: {currentMana}";
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
