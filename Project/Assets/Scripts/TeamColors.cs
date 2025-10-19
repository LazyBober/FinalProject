using UnityEngine;

public class TeamColors : MonoBehaviour
{
    [SerializeField] private Color teamColor;

    public Color TeamColor
    {
        get
        {
            return teamColor;
        }
        set
        {
            teamColor = value;
        }
    }
}
