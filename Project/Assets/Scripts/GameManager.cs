using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CharacterScript> _charactersOnScene = new List<CharacterScript>();
    [SerializeField] private TextMeshProUGUI charactersOnSceneText;

    public void Action()
    {
        //tut vse sho proishodit mizh hodamy gravciv
        for (int i = 0; i < _charactersOnScene.Count; i++)
        {
            _charactersOnScene[0].Special();
        }
    }

    public void Update()
    {
        charactersOnSceneText.text = $"characters on scene: {_charactersOnScene.Count}";
    }

    public void AddCharacter(CharacterScript character)
    {
        _charactersOnScene.Add(character);
    }
}
