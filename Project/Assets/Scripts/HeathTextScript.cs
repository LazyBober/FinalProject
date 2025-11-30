using UnityEngine;
using TMPro;

public class ShowOnCharacterHover : MonoBehaviour
{
    [SerializeField] private GameObject healthTextObject;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        healthTextObject = gameObject;
        healthText = healthTextObject.GetComponent<TextMeshProUGUI>();



        healthTextObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool overCharacter = false;

        if (Physics.Raycast(ray, out hit))
        {
            CharacterScript character = hit.collider.GetComponent<CharacterScript>();
            if (character != null)
            {
                overCharacter = true;
            }
        }

        healthTextObject.SetActive(overCharacter);
    }
}