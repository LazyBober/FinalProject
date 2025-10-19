using UnityEngine;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Prefab to place")]
    [SerializeField] private GameObject placePrefab;

    [Header("Options")]
    [SerializeField] private ManaScript manaScript;
    [SerializeField] private TeamColors teamColors;

    private bool useGrid = true;
    private float gridSize = 1f;
    private Color teamColor;
    private bool placing = false;
    private GameObject newPrefab;

    private void Start()
    {
        teamColors.TeamColor = Color.blue;
    }
    private void Update()
    {
        teamColor = teamColors.TeamColor;
        if (!placing)
        {
            return;
        }
        // Follow mouse
        Vector3 pos = GetMouseWorldPosition();
        if (useGrid)
        {
            pos = SnapToGrid(pos);
        }
        newPrefab.transform.position = pos;

        // Rotate with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            newPrefab.transform.Rotate(0, 0, 90);
        }

        // Left click = confirm place
        if (Input.GetMouseButtonDown(0))
        {
            SpriteRenderer newPrefabRenderer =
            Instantiate(placePrefab, newPrefab.transform.position, newPrefab.transform.rotation).GetComponent<SpriteRenderer>();
            newPrefabRenderer.material.color = teamColor;
            teamColors.TeamColor = teamColor == Color.blue ? Color.red : Color.blue;
            Destroy(newPrefab);
            placing = false; // exit placement mode
        }

        // Right click = cancel
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(newPrefab);
            placing = false;
        }
    }

    private static Vector3 GetMouseWorldPosition()
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

    public void StartPlacing()
    {
        if (manaScript.currentMana >= 3)
        {
            placing = true;

            if (newPrefab == null)
            {
                newPrefab = Instantiate(placePrefab);

            }

            manaScript.currentMana -= 3;
        }
    }
}
