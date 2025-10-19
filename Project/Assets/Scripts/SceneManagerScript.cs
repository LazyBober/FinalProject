using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void OpenSettings()
    {
        SceneManager.LoadScene(0);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenInventory()
    {
        SceneManager.LoadScene(2);
    }
    public void Local2Players ()
    {
        SceneManager.LoadScene(3);
    }
}
