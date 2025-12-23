using UnityEngine;

public class CartBlue : MonoBehaviour
{
    [SerializeField] private float step = 0.5f;
    private bool movedThisTurn = false;

    public void TryMove(Color placedColor)
    {
        if (movedThisTurn)
            return;

        if (placedColor != Color.blue)
            return;

        Vector3 pos = transform.position;
        pos.x -= step;
        transform.position = pos;

        movedThisTurn = true;
    }

    public void ResetTurn()
    {
        movedThisTurn = false;
    }
}
