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
    public GameObject spawnmanager;
    private TimerCountdown gameTimer;
    
    public static GameManager Instance;
    
    private bool _gameOver = false;
    public int scoreP1 = 0;
    public int scoreP2 = 0;
    public TextMeshPro scoreP1text;
    public TextMeshPro scoreP2text;
    public TextMeshPro scoreText; // reference to UI score text component

    public int scorePointsP1 = 1; // how many points player scores when they hit the other player
    public int scorePointsP2 = 1;

    private PlayerMovementScript player1;
    private PlayerMovementScript player2;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        SetPageState(PageState.None);
            
        player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerMovementScript>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerMovementScript>();

        spawnmanager = GameObject.FindWithTag("SpawnManager");
        if (spawnmanager == null)
        {
            Debug.LogError("Spawnmanager object not found!");
        }
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
    
    public void OnReplay()
        // activated when replay button is hit
    {
        SetPageState(PageState.Start);
    }
    
    /// START COUNTDOWN 3 2 1 ///
    public void OnCountdownStart()
    {
        SetPageState(PageState.Countdown);
    }
    
    /// GAME START FUNCTION ///
    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        _gameOver = false;
        
        // reset score
        scoreP1 = 0;
        scoreP2 = 0;
        SetScoreText();
        
        // unfreeze player & enable shooting
        player1.OnGameStarted();
        player2.OnGameStarted();
        
        // start spawning power ups
        spawnmanager.GetComponent<SpawnManager>().activateSpawning();
        
    }
    
    
    /// GAME OVER FUNCTION ///
    public void OnTimeIsOver()
    {
        _gameOver = true;
        SetPageState(PageState.GameOver);
        
        // state who won

        TMP_Text textP1 = gameOverPageP1.GetComponent<TMP_Text>();
        TMP_Text textP2 = gameOverPageP2.GetComponent<TMP_Text>();
        
        if (scoreP1 > scoreP2)
        {
            textP1.SetText("Game Over ! You won !");
            textP2.SetText("<color=red>Game Over ! You lost !</color> ");
        }
        else if (scoreP1 == scoreP2)
        {
            textP1.SetText("Game Over ! It's a tie !");
            textP2.SetText("Game Over ! It's a tie !");
        }
        else
        {
            textP1.SetText("<color=red>Game Over ! You lost !</color> ");
            textP2.SetText("Game Over ! You won !");
        }

        player1.OnGameOverConfirmed();
        player2.OnGameOverConfirmed();
        
        // stop spawning power ups
        spawnmanager.GetComponent<SpawnManager>().deactivateSpawning();
        
        // deactivate active power ups and balls
        foreach (Transform child in spawnmanager.transform)
            child.gameObject.SetActive(false);
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
    

    
}
