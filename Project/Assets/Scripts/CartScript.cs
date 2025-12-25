using UnityEngine;


public class CartScript : MonoBehaviour
{
    [SerializeField] private Sprite pushingShape;
    [SerializeField] private GameObject _cart;
    [SerializeField] private Color _enemyColor;
    [SerializeField] private Transform _destination;
    [SerializeField] private Color _cartColor;
    private bool _gameOver = false;
    [SerializeField] private SceneManagerScript sceneManager;

    private bool movable = true;

    [SerializeField] private string team;

    void Start()
    {
        _cart.GetComponent<SpriteRenderer>().material.color = _cartColor;
        
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
        CharacterScript spriteRenderer = collision.GetComponent<CharacterScript>();
        if (spriteRenderer._canPushCart == true)
        {
            Color collisionColor = collision.GetComponent<SpriteRenderer>().material.color;
            if (_cart.GetComponent<CartScript>().team == spriteRenderer.team && movable)
            {
                if (_cartColor == Color.red)
                {
                    _cart.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
                }
                else
                {
                    _cart.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
                }
            }
            else if (_cart.GetComponent<CartScript>().team != spriteRenderer.team && )
            {
                movable = false;
            }
            else if (!movable)
            {
                Debug.Log("Enemy object blocked the way");
            }
        }
    }

    private void GameOver()
    {
        if (_cartColor == Color.red)
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