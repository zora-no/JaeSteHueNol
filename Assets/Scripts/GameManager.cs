using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    
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
        //scoreP1text = scoreP1.ToString();
        //scoreP2text = scoreP2.ToString();
        Debug.Log("SetscoreP1: " + scoreP1);
        Debug.Log("SetscoreP2: " + scoreP2);
        //scoreP2text.color = new Color32(0, 255, 33, 255);
        //scoreP1text.color = new Color32(255, 0, 14, 255);
        scoreP1text.text = scoreP1.ToString();
        scoreP2text.text = scoreP2.ToString();
        //scoreText.text = scoreP1text + " : " + scoreP2text;
        // scoreText.text = scoreP1.ToString() + " : " + scoreP2.ToString();
        
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
    
    public void OnPlayerDied(int player)
    {
        _gameOver = true;
        SetPageState(PageState.GameOver);
        // ...
    }

    public void OnPlayerScored(int player)
        // manages score on screen
    {
        if (player == 1)
        {
            scoreP1++;
        }
        else if (player == 2)
        {
            scoreP2++;
        }
        Debug.Log("UpdatescoreP1: " + scoreP1);
        Debug.Log("UpdatescoreP2: " + scoreP2);
        SetScoreText();
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
