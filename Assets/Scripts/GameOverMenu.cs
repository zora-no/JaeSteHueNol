using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    GameManager GameManager;


    void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        
    }

    public void GoBackToMainMenu()
    {
        GameManager.OnBackToMainMenu();
    }
    
    public void StartAgain()
    {
        GameManager.OnGameStart();
    }
}
