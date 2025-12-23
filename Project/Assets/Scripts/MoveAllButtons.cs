using UnityEngine;
using UnityEngine.UI;

public class MoveAllButtons : MonoBehaviour
{
    [SerializeField] GridLayoutGroup glg;
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
        RectTransform rect = glg.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;

        pos.x += moveRight ? 260f : -260f;
        rect.anchoredPosition = pos;

        glg.startCorner = moveRight ? GridLayoutGroup.Corner.UpperRight : GridLayoutGroup.Corner.UpperLeft;

        moveRight = !moveRight;
    }
}