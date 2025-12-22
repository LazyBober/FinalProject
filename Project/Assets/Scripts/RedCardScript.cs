using UnityEngine;

public class RedCartScript : MonoBehaviour
{
    [Header("Prefab that can push this cart")]
    [SerializeField] private GameObject allowedShapePrefab;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool moveRight = true;

    private bool canMove = false;

    private string allowedPrefabName;

    private void Awake()
    {
        // сохраняем имя префаба
        if (allowedShapePrefab != null)
            allowedPrefabName = allowedShapePrefab.name;
    }

    private void Update()
    {
        if (!canMove) return;

        float dir = moveRight ? 1f : -1f;
        transform.position += new Vector3(dir * moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedShapePrefab == null) return;

        // Проверка: объект создан из нужного префаба
        if (collision.gameObject.name == allowedPrefabName + "(Clone)")
        {
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (allowedShapePrefab == null) return;

        if (collision.gameObject.name == allowedPrefabName + "(Clone)")
        {
            canMove = false;
        }
    }
}

