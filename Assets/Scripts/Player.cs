using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private GameManager game;
    
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;


    void Start()
    {
        game = GameManager.Instance;
    }

    void Update()
    {
        
    }
    
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void onDisenable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }
    
    void OnGameStarted()
    {
        // ...
    }

    void OnGameOverConfirmed() 
    {
        // ...
    }
    
    void OnTriggerEnter(Collider other)
        // check for collision with other game objects
    {
        OnPlayerDied();
        OnPlayerScored();
    }
    
    
}
