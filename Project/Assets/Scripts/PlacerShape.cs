using UnityEngine;

public class PlacedShape : MonoBehaviour
{
    public Color shapeColor;
    public bool isPlaced;

    private Collider2D col;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Пока фигура таскается — коллайдера нет
        if (col != null)
            col.enabled = false;
    }

    public void Place(Color teamColor)
    {
        shapeColor = teamColor;
        isPlaced = true;

        if (sr != null)
            sr.material.color = teamColor;

        if (col != null)
            col.enabled = true;
    }
}

