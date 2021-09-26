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

    public void GoBackToMainMenu()
    {
        GameManager.OnBackToMainMenu();
    }
    
    public void StartAgain()
    {
        GameManager.OnGameStart();
    }
    
    //public void StateWinner()
    //{
    //    winnerStatementLeft.text = "winner";
    //    winnerStatementRight.text = "looser";
    // }
    

}
