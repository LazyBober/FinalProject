


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ThemeSwitch", menuName = "ThemeSwitch")]
public class ThemeData : ScriptableObject
{
    [SerializeField] int theme;

    public int Theme
    {
        get => theme;
        set => theme = value;
    }
}