﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Build.Content;
using System.IO;

public class GameManager : MonoBehaviour
{

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject TimerPage;
    public GameObject spawnmanager;
    public GameObject threeTwoOnePage;
    private TimerCountdown gameTimer;
    private CountDownBeginning countDownBeginning;

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

    private BallThrowScript throwingP1;
    private BallThrowScript throwingP2;

    public string nameP1;
    public string nameP2;

    public TextMeshProUGUI History1;
    public TextMeshProUGUI History2;
    public TextMeshProUGUI History3;
    public TextMeshProUGUI History4;
    public TextMeshProUGUI History5;

    public TextMeshProUGUI WinnerStatementP1;
    public TextMeshProUGUI WinnerStatementP2;

    public bool startTimer = false;

    private StreamWriter writer;

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
        
        throwingP1 = GameObject.FindWithTag("Player1").GetComponent<BallThrowScript>();
        throwingP2 = GameObject.FindWithTag("Player2").GetComponent<BallThrowScript>(); 
        
        nameP1 = "";
        nameP2 = "";
        
        throwingP1.deactivateShooting();
        throwingP2.deactivateShooting();

        spawnmanager = GameObject.FindWithTag("SpawnManager");
        if (spawnmanager == null)
        {
            Debug.LogError("Spawnmanager object not found!");
        }
        SetScoreText();

        startTimer = false;
        gameTimer = TimerPage.GetComponent<TimerCountdown>();
        countDownBeginning = threeTwoOnePage.GetComponent<CountDownBeginning>();


        UpdateScoreHistory();

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        } 
    }
    

    enum PageState
    // the five page states
    {
        None,
        Start,
        GameOver,
        Timer,
        ThreeTwoOneGo
    }

    public void SetScoreText()
    {
        scoreP1text.text = scoreP1.ToString();
        scoreP2text.text = scoreP2.ToString();
    }

    /// START GAME COUNTDOWN ///
    public void StartCountDown()
    {
        disablePowerUpsUsed();
        throwingP1.deactivateShooting();
        throwingP2.deactivateShooting();
        SetPageState(PageState.ThreeTwoOneGo);
        countDownBeginning.ShowCountdown();
        
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
        SetPageState(PageState.Timer);
        _gameOver = false;
        
        gameTimer.onetime = false;
        gameTimer.ResetTimer();
        


        // reset score
        scoreP1 = 0;
        scoreP2 = 0;
        SetScoreText();

        disablePowerUpsUsed();
        
        // unfreeze player & enable shooting
        player1.OnGameStarted();
        player2.OnGameStarted();

        // start spawning power ups
        spawnmanager.GetComponent<SpawnManager>().activateSpawning();
        spawnmanager.GetComponent<SpawnManager>().onGameStart();

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

        // add game result to score history file
        WriteToFile();

        // update score history in menu
        UpdateScoreHistory();

    }

    /// GAME OVER CONFIRMATION ///
    public void OnBackToMainMenu()
    {
        _gameOver = false;
        startTimer = false;
        SetPageState(PageState.Start);

    }

    void disablePowerUpsUsed()
    {
        // Disable movement inhibitors
        GameObject move = GameObject.FindWithTag("movementinhibitorfield");
        if (move != null)
        {
            do
            {
                move.transform.parent.GetComponent<TileScript>().DeactivateOnGameStart();
                move.transform.parent.GetComponent<TileScript>().effectIsActive = false;
                move = GameObject.FindWithTag("movementinhibitorfield");
            } while (move != null);
        }

        // Disable slow fields
        GameObject slow = GameObject.FindWithTag("slowfield");
        if (slow != null)
        {
            do
            {
                slow.transform.parent.GetComponent<TileScript>().DeactivateOnGameStart();
                slow.transform.parent.GetComponent<TileScript>().effectIsActive = false;
                slow = GameObject.FindWithTag("slowfield");
            } while (slow != null);
        }

        // Disable clouded fields
        GameObject cloud = GameObject.FindWithTag("cloudedviewfield");
        if (cloud != null)
        {
            do
            {
                cloud.transform.parent.GetComponent<TileScript>().DeactivateOnGameStart();
                cloud.transform.parent.GetComponent<TileScript>().effectIsActive = false;
                cloud = GameObject.FindWithTag("cloudedviewfield");
            } while (cloud != null);
        }
        
        // Unload saved powerups
        player1.GetComponent<PlayerMovementScript>().ResetTileEffectType();
        player2.GetComponent<PlayerMovementScript>().ResetTileEffectType();
        GameObject.Find("TilePowerup2").GetComponent<uiTilePowerups>().SwitchImage(3);
        GameObject.Find("TilePowerup1").GetComponent<uiTilePowerups>().SwitchImage(3);
    }

    void WriteToFile()
    {
        string line = nameP1 + "  " + scoreP1 + "  :  " + scoreP2 + "  " + nameP2;
        // + "\n"

        writer = new StreamWriter("GameHistory.txt", true);
        writer.WriteLine(line);
        writer.Close();
    }


    void UpdateScoreHistory()
    {
        string[] records = File.ReadAllLines("GameHistory.txt");

        int N = records.Length;
        if (N > 0) { History1.text = records[N - 1]; }
        if (N > 1) { History2.text = records[N - 2]; }
        if (N > 2) { History3.text = records[N - 3]; }
        if (N > 3) { History4.text = records[N - 4]; }
        if (N > 4) { History5.text = records[N - 5]; }
    }


    public bool getGameOver()
    {
        return _gameOver;
    }

    // manages score on screen
    public void OnPlayerScored(string player)
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
                gameOverPage.SetActive(false);
                TimerPage.SetActive(false);
                threeTwoOnePage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                TimerPage.SetActive(false);
                threeTwoOnePage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                TimerPage.SetActive(false);
                threeTwoOnePage.SetActive(false);
                break;
            case PageState.Timer:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                TimerPage.SetActive(true);
                threeTwoOnePage.SetActive(false);
                break;
            case PageState.ThreeTwoOneGo:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                TimerPage.SetActive(false);
                threeTwoOnePage.SetActive(true);
                break;
        }
    }

    public void readNameP1(string input)
    {
        nameP1 = input;
    }

    public void readNameP2(string input)
    {
        nameP2 = input;
    }
    
    // states tie or winner as indicated by score 
    private void stateWinner()
    {

        if (scoreP1 > scoreP2)
        {
            WinnerStatementP1.text = nameP1 + ", you won my friend!";
            WinnerStatementP2.text = nameP2 + ", you lost!";
        }
        else if (scoreP1 == scoreP2)
        {
            WinnerStatementP1.text = "It's a Tie";
            WinnerStatementP2.text = "It's a Tie";
        }
        else
        {
            WinnerStatementP1.text = nameP1 + ", you lost!";
            WinnerStatementP2.text = nameP2 + ", you won my friend!";
        }
    }

}
