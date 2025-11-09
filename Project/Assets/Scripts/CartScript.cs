using UnityEngine;


public class CartScript : MonoBehaviour
{
    [SerializeField] private GameObject _cart;
    [SerializeField] private Color _enemyColor;
    [SerializeField] private Transform _destination;
    [SerializeField] private Color cartColor;
    private bool _gameOver = false;
    [SerializeField] private SceneManagerScript sceneManager;

    void Start()
    {
        if (_cart.transform.position == new Vector3(-4.5f, -2.5f, 0f))
        {
            _cart.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else
        {
            _cart.GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
        cartColor = _cart.GetComponent<SpriteRenderer>().material.color;
        if (cartColor == Color.red)
        {
            _enemyColor = Color.blue;
        }
        else if (cartColor == Color.blue)
        {
            _enemyColor = Color.red;
        }
    }

    void Update()
    {
        if (_cart.transform.position == _destination.transform.position)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color collisionColor = collision.GetComponent<SpriteRenderer>().material.color;
        if (collisionColor == cartColor)
        {
            if (cartColor == Color.red)
            {
                _cart.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            }
            else
            {
                _cart.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            }
        }
    }

    private void GameOver()
    {
        if (cartColor == Color.red)
        {
            Debug.Log("red won gg");
        }
        else
        {
            Debug.Log("Blue won gg");
        }
        sceneManager.BackToMenu();
    }
}
