using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField]
    GameManager GameManager;

    void Start()
    {
        if (GameManager == null)
        {
            Debug.LogError("GameManager object missing");
        }
    }
    // when pressing the button game will start
    public void PlayGame ()
    {
        GameManager.OnCountdownStart();
    }
    
    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
