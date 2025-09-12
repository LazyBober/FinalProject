using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class GridCharacterPlacementTest : MonoBehaviour
{
    [Header("Prefab to place")]
    [SerializeField] private GameObject placePrefab;

    [Header("Options")]
    public bool useGrid = true;
    public float gridSize = 1f;

    private bool placing = false;
    private GameObject newPrefab;

    [SerializeField] private ManaScript manaScript;

    private void Update()
    {
        if (!placing) return;

        // Follow mouse
        Vector3 pos = GetMouseWorldPosition();
        if (useGrid) pos = SnapToGrid(pos);
        newPrefab.transform.position = pos;

        // Rotate with R
        if (Input.GetKeyDown(KeyCode.R))
            newPrefab.transform.Rotate(0, 0, 90);

        // Left click = confirm place
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(placePrefab, newPrefab.transform.position, newPrefab.transform.rotation);
            Destroy(newPrefab);
            newPrefab = null;
            placing = false; // exit placement mode
        }

        // Right click = cancel
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(newPrefab);
            newPrefab = null;
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
