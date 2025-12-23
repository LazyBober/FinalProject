using UnityEngine;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Prefab to place")]
    [SerializeField] private GameObject placePrefabRed;
    [SerializeField] private GameObject placePrefabBlue;
    private GameObject placePrefab;

    [Header("Options")]
    [SerializeField] private ManaScript manaScriptBlue;
    [SerializeField] private ManaScript manaScriptRed;
    [SerializeField] private TeamColors teamColors;

    private bool useGrid = true;
    private float gridSize = 0.5f;
    private Color teamColor;
    private bool placing = false;
    private GameObject newPrefab;

    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        teamColors.TeamColor = Color.blue;
        placePrefab = placePrefabBlue;
    }
    private void Update()
    {
        teamColor = teamColors.TeamColor;
        if (!placing)
        {
            return;
        }

        Vector3 pos = GetMouseWorldPosition();
        if (useGrid)
        {
            pos = SnapToGrid(pos);
        }
        newPrefab.transform.position = pos;


        if (Input.GetKeyDown(KeyCode.R))
        {
            newPrefab.transform.Rotate(0, 0, 90);
        }

        // Left click = confirm place
        if (Input.GetMouseButtonDown(0))
        {
            if (teamColor == Color.blue)
            {
                placePrefab = placePrefabBlue;
            }
            if (teamColor == Color.red)
            {
                placePrefab = placePrefabRed;
            }
            SpriteRenderer newPrefabRenderer =
            Instantiate(placePrefab, newPrefab.transform.position, newPrefab.transform.rotation).GetComponent<SpriteRenderer>();

            teamColors.TeamColor = teamColor == Color.blue ? Color.red : Color.blue;
            Destroy(newPrefab);
            placing = false;
            if (teamColor == Color.red)
            {
                manaScriptRed.currentMana += 1;
            }
            if (teamColor == Color.blue)
            {
                manaScriptBlue.currentMana += 1;
            }
            gameManager.AddCharacter(newPrefabRenderer.GetComponent<CharacterScript>());
            gameManager.Action();

        }

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
        if (teamColor == Color.blue)
        {
            if (manaScriptBlue.currentMana >= 2)
            {
                placing = true;

                if (newPrefab == null)
                {
                    newPrefab = Instantiate(placePrefab);

                }

                manaScriptRed.currentMana -= 2;

            }
        }
        else if (teamColor == Color.red)
        {
            if (manaScriptRed.currentMana >= 2)
            {
                placing = true;

                if (newPrefab == null)
                {
                    newPrefab = Instantiate(placePrefab);

                }

                manaScriptBlue.currentMana -= 2;

            }
        }
    }

    public void SkipTurn()
    {
        if (teamColor == Color.red)
        {
            manaScriptRed.currentMana += 2;
        }
        if (teamColor == Color.blue)
        {
            manaScriptBlue.currentMana += 2;
        }
        teamColors.TeamColor = teamColor == Color.blue ? Color.red : Color.blue;
        gameManager.Action();
    }
}
