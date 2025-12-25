using UnityEngine;
using UnityEngine.UI;

public class MoveAllButtons : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private bool moveRight = true;

    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(MoveButtons);
        }
    }

    public void MoveButtons()
    {
        foreach (Button btn in buttons)
        {
            RectTransform rect = btn.GetComponent<RectTransform>();
            Vector2 pos = rect.anchoredPosition;

            float direction = moveRight ? 2.1f : -2.1f;

            pos.x += 300f * direction;

            rect.anchoredPosition = pos;
        }

        moveRight = !moveRight;
    }
}