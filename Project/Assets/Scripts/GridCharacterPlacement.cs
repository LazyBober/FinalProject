using UnityEngine;

public class GridCharacterPlacement : MonoBehaviour
{
    [SerializeField] private GameObject placePrefab;
    [SerializeField] private TeamColors teamColors;
    [SerializeField] private GameManager gameManager;

    private GameObject preview;
    private bool placing;

    private float gridSize = 0.5f;

    private void Update()
    {
        if (!placing || preview == null) return;

        preview.transform.position = SnapToGrid(GetMouseWorldPosition());

        if (Input.GetMouseButtonDown(0))
            Confirm();

        if (Input.GetMouseButtonDown(1))
            Cancel();
    }

    public void StartPlacing()
    {
        placing = true;
        preview = Instantiate(placePrefab);
        DisableCollider(preview);
    }

    private void Confirm()
    {
        EnableCollider(preview);

        PlacedShape shape = preview.GetComponent<PlacedShape>();
        if (shape != null)
            shape.Place(teamColors.TeamColor);

        teamColors.TeamColor =
            teamColors.TeamColor == Color.blue ? Color.red : Color.blue;

        placing = false;
        preview = null;

        if (gameManager != null)
            gameManager.Action();
    }

    private void Cancel()
    {
        Destroy(preview);
        placing = false;
    }

    private void DisableCollider(GameObject obj)
    {
        Collider2D col = obj.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    private void EnableCollider(GameObject obj)
    {
        Collider2D col = obj.GetComponent<Collider2D>();
        if (col != null) col.enabled = true;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screen = Input.mousePosition;
        screen.z = 10f;
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


