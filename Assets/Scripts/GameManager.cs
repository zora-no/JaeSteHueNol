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
    public GameObject TimerPage;
    public GameObject spawnmanager;
    public GameObject threeTwoOnePage;
    private TimerCountdown gameTimer;
    
    public static GameManager Instance;
    
    private bool _gameOver = false;
    public int scoreP1 = 0;
    public int scoreP2 = 0;
    public TextMeshPro scoreP1text;
    public TextMeshPro scoreP2text;

    public int scorePointsP1 = 1; // how many points player scores when they hit the other player
    public int scorePointsP2 = 1;

    private PlayerMovementScript player1;
    private PlayerMovementScript player2;

    private List<string> namesP1 = new List<string>();
    private List<string> namesP2 = new List<string>();
    public string nameP1;
    public string nameP2;
    
    private List<string> scoresP1 = new List<string>();
    private List<string> scoresP2 = new List<string>();
    
    public TextMeshProUGUI History1;
    public TextMeshProUGUI History2;
    public TextMeshProUGUI History3;
    public TextMeshProUGUI History4;
    public TextMeshProUGUI History5;

    public bool startTimer = false;
    
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _gameOver = false;
        SetPageState(PageState.Start);
            
        player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerMovementScript>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerMovementScript>();
        nameP1 = "";
        nameP2 = "";

        spawnmanager = GameObject.FindWithTag("SpawnManager");
        if (spawnmanager == null)
        {
            Debug.LogError("Spawnmanager object not found!");
        }
        SetScoreText();
        startTimer = false;
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
        Timer
    }

    public void SetScoreText()
    {
        scoreP1text.text = scoreP1.ToString();
        scoreP2text.text = scoreP2.ToString();
    }
    
    /// START GAME TIMER ///
    public void StartTimer()
    {
        SetPageState(PageState.Timer);
        startTimer = true;
    }
    
    /// GAME START FUNCTION ///
    public void OnGameStart()
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
        stateWinner();

        player1.OnGameOverConfirmed();
        player2.OnGameOverConfirmed();
        
        // stop spawning power ups
        spawnmanager.GetComponent<SpawnManager>().deactivateSpawning();
        
        // deactivate active power ups and balls
        foreach (Transform child in spawnmanager.transform)
            child.gameObject.SetActive(false);
        
        // save scores
        scoresP1.Insert(0, scoreP1.ToString());
        scoresP2.Insert(0, scoreP2.ToString());
        
        // make sure the saved lists don't have more than 5 entries to avoid cluttering
        restrictListSize();
        
        // update game history page
        updateGameHistoryPage();
        
        nameP1 = "";
        nameP2 = "";
    }

    
    public bool getGameOver()
    {
        return _gameOver;
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
                TimerPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPageP1.SetActive(false);
                gameOverPageP2.SetActive(false);
                TimerPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPageP1.SetActive(true);
                gameOverPageP2.SetActive(true);
                TimerPage.SetActive(false);
                break;
            case PageState.Timer:
                startPage.SetActive(false);
                gameOverPageP1.SetActive(false);
                gameOverPageP2.SetActive(false);
                TimerPage.SetActive(true);
                break;
        }
    }

    public void readNameP1(string input)
    {
        namesP1.Insert(0, input);
        nameP1 = input;
    }
    
    public void readNameP2(string input)
    {
        namesP2.Insert(0, input);
        nameP2 = input;
    }
    
    private void restrictListSize()
    {
        if (scoresP1.Count == 6)
        {
            scoresP1.RemoveAt(5);
        }
        if (scoresP2.Count == 6)
        {
            scoresP2.RemoveAt(5);
        }
        if (namesP1.Count == 6)
        {
            namesP1.RemoveAt(5);
        }
        if (namesP2.Count == 6)
        {
            namesP2.RemoveAt(5);
        }
    }

    private void updateGameHistoryPage()
    {
        if (namesP1.Count > 0)
        {
            History1.text = namesP1[0] + "  " + scoresP1[0] + "  :  " + scoresP2[0] + "  " + namesP2[0];
        }
        if (namesP1.Count > 1)
        {
            History2.text = namesP1[1] + "  " + scoresP1[1] + "  :  " + scoresP2[1] + "  " + namesP2[1];
        }
        if (namesP1.Count > 2)
        {
            History3.text = namesP1[2] + "  " + scoresP1[2] + "  :  " + scoresP2[2] + "  " + namesP2[2];
        }
        if (namesP1.Count > 3)
        {
            History4.text = namesP1[3] + "  " + scoresP1[3] + "  :  " + scoresP2[3] + "  " + namesP2[3];
        }
        if (namesP1.Count > 4)
        {
            History5.text = namesP1[4] + "  " + scoresP1[4] + "  :  " + scoresP2[4] + "  " + namesP2[4];
        }
        
    }


    private void stateWinner()
    {
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
    }

    
    
    
}
