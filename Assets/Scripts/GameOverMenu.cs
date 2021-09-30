using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{

    GameManager GameManager;
    public TextMeshProUGUI winnerStatementLeft;
    public TextMeshProUGUI winnerStatementRight;
    


    void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        
    }

    // after game over go back to main menu 
    public void GoBackToMainMenu()
    {
        GameManager.OnBackToMainMenu();
    }
    // after game over start new round
    public void StartAgain()
    {
        GameManager.OnGameStart();
    }
    
    

}
