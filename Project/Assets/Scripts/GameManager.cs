using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CharacterScript> _charactersOnScene = new List<CharacterScript>();
    [SerializeField] private GameObject CartRed;
    [SerializeField] private GameObject CartBlue;

    public void Action()
    {
        //tut vse sho proishodit mizh hodamy gravciv
        for (int i = 0; i < _charactersOnScene.Count; i++)
        {
            _charactersOnScene[i].Special();
        }
           
    }

    public void AddCharacter(CharacterScript character)
    {
        _charactersOnScene.Add(character);
    }
}
