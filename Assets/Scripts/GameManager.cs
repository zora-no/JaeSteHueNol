using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    
    public static GameManager Instance;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    
    private bool gameOver = false;
    
    void Awake()
    {
        Instance = this;
    }
    
    enum PageState
        // the four page states
    {
        None,
        Start,
        GameOver,
        Countdown
    }
    
    void OnCountdownFinished()
        // once countdown is finished, get everything ready for game start
    {
        SetPageState(PageState.None);
        gameOver = false;
        OnGameStarted(); // event sent to Player
        //score = 0;
        // ...
    }
    
    void OnPlayerDied()
    {
        gameOver = true;
        SetPageState(PageState.GameOver);
        // ...
    }

    void OnPlayerScored()
        // manages score on screen
    {
        // ...
    }
    

    void SetPageState(PageState state)
        // activate needed page and deactivate all others
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
        // activated when replay button is hit
    {
        OnGameOverConfirmed(); // event sent to Player
        SetPageState(PageState.Start);
        // ...
    }

    public void StartGame()
        // activated when play button is hit
    {
        SetPageState(PageState.Countdown);
    }

    
}
