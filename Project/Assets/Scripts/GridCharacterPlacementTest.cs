using UnityEngine;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject placePrefab;

    [Header("Mana")]
    [SerializeField] private ManaScript manaBlue;
    [SerializeField] private ManaScript manaRed;

    [Header("Turn")]
    [SerializeField] private TeamColors teamColors;
    [SerializeField] private GameManager gameManager;

    private GameObject previewPrefab;
    private bool placing = false;
    private float gridSize = 0.5f;

    private void Start()
    {
        teamColors.TeamColor = Color.blue;
    }

    private void Update()
    {
        if (!placing || previewPrefab == null)
            return;

        Vector3 pos = GetMouseWorldPosition();
        pos = SnapToGrid(pos);
        previewPrefab.transform.position = pos;

        // Подтвердить установку
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }

        // Отмена
        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    public void StartPlacing()
    {
        Color teamColor = teamColors.TeamColor;

        if (placing)
            return;

        if (teamColor == Color.blue && manaBlue.currentMana < 2)
            return;

        if (teamColor == Color.red && manaRed.currentMana < 2)
            return;

        placing = true;
        previewPrefab = Instantiate(placePrefab);

        // Отключаем коллайдер во время перетаскивания
        Collider2D col = previewPrefab.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Цвет превью
        previewPrefab.GetComponent<SpriteRenderer>().material.color = teamColor;

        if (teamColor == Color.blue)
            manaBlue.currentMana -= 2;
        else
            manaRed.currentMana -= 2;
    }

    private void PlaceObject()
    {
        Color teamColor = teamColors.TeamColor;

        GameObject placed = Instantiate(
            placePrefab,
            previewPrefab.transform.position,
            previewPrefab.transform.rotation
        );

        PlacedShape character = placed.GetComponent<PlacedShape>();
        character.Place(teamColor);

        // 🔥 ДВИЖЕНИЕ ТЕЛЕЖЕК (ТОЛЬКО 1 РАЗ)
        CartBlue blueCart = FindObjectOfType<CartBlue>();
        if (blueCart != null)
            blueCart.TryMove(teamColor);

        CartRed redCart = FindObjectOfType<CartRed>();
        if (redCart != null)
            redCart.TryMove(teamColor);

        Destroy(previewPrefab);
        placing = false;

        teamColors.TeamColor = teamColor == Color.blue ? Color.red : Color.blue;
        gameManager.Action();
    }

    private void CancelPlacement()
    {
        Color teamColor = teamColors.TeamColor;

        Destroy(previewPrefab);
        placing = false;

        if (teamColor == Color.blue)
            manaBlue.currentMana += 2;
        else
            manaRed.currentMana += 2;
    }

    public void SkipTurn()
    {
        Color teamColor = teamColors.TeamColor;

        if (teamColor == Color.blue)
            manaBlue.currentMana += 2;
        else
            manaRed.currentMana += 2;

        teamColors.TeamColor = teamColor == Color.blue ? Color.red : Color.blue;
        gameManager.Action();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);
        world.z = 0f;
        return world;
    }

    private Vector3 SnapToGrid(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x / gridSize) * gridSize,
            Mathf.Round(pos.y / gridSize) * gridSize,
            0f
        );
    }
}

