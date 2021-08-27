using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public GameObject startPage;
    public GameObject gameOverPageP1;
    public GameObject gameOverPageP2;
    public GameObject countdownPage;
    private TimerCountdown gameTimer;
    
    public static GameManager Instance;


    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    
    private bool _gameOver = false;
    public int scoreP1 = 1;
    public int scoreP2 = 0;
    public TextMeshPro scoreP1text;
    public TextMeshPro scoreP2text;
    public TextMeshPro scoreText; // reference to UI score text component

    public int scorePointsP1 = 1; // how many points player scores when they hit the other player
    public int scorePointsP2 = 1;
    
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        SetScoreText();
    }
    void Update()
    {
        //OnPlayerScored("P1");
        //setScoreText();
    }
    
    public bool GameOver { get { return _gameOver; } } 
    
    public int ScoreP1 { get { return scoreP1; } }
    public int ScoreP2 { get { return scoreP2; } }
    
    enum PageState
        // the four page states
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    public void SetScoreText()
    {
        scoreP1text.text = scoreP1.ToString();
        scoreP2text.text = scoreP2.ToString();
    }
    
    void OnCountdownFinished()
        // once countdown is finished, get everything ready for game start
    {
        SetPageState(PageState.None);
        _gameOver = false;
        OnGameStarted(); // event sent to Player
        //score = 0;
        // ...
    }
    
    // not used because player does't die
    //public void OnPlayerDied(int player)
    //{
    //    _gameOver = true;
    //    SetPageState(PageState.GameOver);
        // ...
    //}
    
    public void OnTimeIsOver()
    {
        _gameOver = true;
        SetPageState(PageState.GameOver);
        
        // state who won
        if (scoreP1 > scoreP2)
        {
            gameOverPageP1.GetComponent<TMP_Text>().SetText("Game Over ! You won !");
            gameOverPageP2.GetComponent<TMP_Text>().SetText("<color=red>Game Over ! You lost !</color> ");
        }
        else if (scoreP1 == scoreP2)
        {
            gameOverPageP1.GetComponent<TMP_Text>().SetText("Game Over ! It's a tie !");
            gameOverPageP2.GetComponent<TMP_Text>().SetText("Game Over ! It's a tie !");
        }
        else
        {
            gameOverPageP1.GetComponent<TMP_Text>().SetText("<color=red>Game Over ! You lost !</color> ");
            gameOverPageP2.GetComponent<TMP_Text>().SetText("Game Over ! You won !");
        }

        // deactivate Powerups, Shooting and movement missing
    }

    public void OnPlayerScored(string player)
        // manages score on screen
    {
        if (player == "Player1")
        {
            scoreP1 += scorePointsP1;
        }
        else if (player == "Player2")
        {
            scoreP2 += scorePointsP2;
        }
        SetScoreText();
    }
    
    

    void SetPageState(PageState state)
        // activate needed page and deactivate all others
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPageP1.SetActive(false);
                gameOverPageP2.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPageP1.SetActive(false);
                gameOverPageP2.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPageP1.SetActive(true);
                gameOverPageP2.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPageP1.SetActive(false);
                gameOverPageP2.SetActive(false);
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
