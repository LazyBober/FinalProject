using UnityEngine;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Prefab to place")]
    public GameObject placePrefab;

    [Header("Options")]
    public KeyCode placeKey = KeyCode.B;   // Press B to place
    public bool useGrid = true;
    public float gridSize = 1f;

    private bool placing = false;
    private GameObject ghost;

    private void Update()
    {
        if (Input.GetKeyDown(placeKey))
        {
            // Start placement mode
            placing = true;

            if (ghost == null)
                ghost = Instantiate(placePrefab);
        }

        if (!placing) return;

        // Follow mouse
        Vector3 pos = GetMouseWorldPosition();
        if (useGrid) pos = SnapToGrid(pos);
        ghost.transform.position = pos;

        // Rotate with R
        if (Input.GetKeyDown(KeyCode.R))
            ghost.transform.Rotate(0, 0, 90);

        // Left click = confirm place
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(placePrefab, ghost.transform.position, ghost.transform.rotation);
            Destroy(ghost);
            ghost = null;
            placing = false; // exit placement mode
        }

        // Right click = cancel
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(ghost);
            ghost = null;
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

    private Vector3 SnapToGrid(Vector3 p)
    {
        return new Vector3(
            Mathf.Round(p.x / gridSize) * gridSize,
            Mathf.Round(p.y / gridSize) * gridSize,
            0f
        );
    }
}
