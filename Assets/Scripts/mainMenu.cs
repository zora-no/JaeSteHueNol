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
    // when pressing the button startPage will be deactivated
    public void PlayGame ()
    {
        GameManager.StartTimer();
        
        Debug.Log("Play!");
        
    }
    
    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
