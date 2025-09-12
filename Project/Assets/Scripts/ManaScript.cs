using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    public int currentMana = 0;
    [SerializeField] private int maxMana = 10;
    [SerializeField] private float giveManaInterval = 2f;

    [SerializeField] private TextMeshProUGUI text;

    public IEnumerator GiveMana()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana++;
            }

            yield return new WaitForSeconds(giveManaInterval);
        }
    }

    private void Start()
    {
        StartCoroutine(GiveMana());
    }

    private void Update()
    {
        text.text = $"mana: {currentMana}";
    }
}
