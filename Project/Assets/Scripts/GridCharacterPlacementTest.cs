using UnityEngine;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterData data;

    [Header("Turn")]
    [SerializeField] private TeamColors teamColors;
    [SerializeField] private GameManager gameManager;

    private GameObject previewPrefab;
    public bool placing = false;
    private float gridSize = 0.5f;

    private void Start()
    {
        previewPrefab = data.PrefabNormal;
        teamColors.TeamColor = Color.blue;
    }

    private void Update()
    {

        ///////////////////////////////////////////////////////////////////////
        // TUT YA VYRISHIV SHO BUDE PROSTO KURSOR ZAMIST PREFABU BEZ KOLYORU //
        ///////////////////////////////////////////////////////////////////////
        
        

        if (!placing)
        {
            return;
        }

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
        {
            return;
        }

        placing = true;
        previewPrefab.SetActive(true);

        // Отключаем коллайдер во время перетаскивания
        Collider2D col = previewPrefab.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

    private void PlaceObject()
    {
        Color teamColor = teamColors.TeamColor;

        GameObject placed = teamColor == Color.blue ? Instantiate(data.PrefabBlue, previewPrefab.transform.position, previewPrefab.transform.rotation)
            : Instantiate(data.PrefabRed, previewPrefab.transform.position, previewPrefab.transform.rotation);


        PlacedShape character = placed.GetComponent<PlacedShape>();
        character.Place(teamColor);

        CharacterScript actualCharacter = placed.GetComponent<CharacterScript>();
        actualCharacter.SetStats(data);

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
    }

    public void SkipTurn()
    {
        Color teamColor = teamColors.TeamColor;
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

