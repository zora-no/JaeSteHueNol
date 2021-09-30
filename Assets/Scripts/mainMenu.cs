using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    GameManager GameManager;

    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _descriptionMenu;
    [SerializeField]
    private GameObject _scoreHistoryMenu;
    [SerializeField]
    private GameObject _nameMenu;
    [SerializeField]
    private GameObject _controleMenu;
    [SerializeField]
    private GameObject _cMenu;
    
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        _mainMenu.SetActive(true);
        _descriptionMenu.SetActive(false);
        _scoreHistoryMenu.SetActive(false);
        _nameMenu.SetActive(false);
        _controleMenu.SetActive(false);
        
    }
    
    // when pressing the button startPage will be deactivated
    public void PlayGame ()
    {
        GameManager.StartTimer();
        GameManager.OnGameStart();
    }

    public void ReactivateMainMenu()
    {
        _mainMenu.SetActive(true);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }

    public void CheckIfNameEntered()
    {
        if (GameManager.nameP1 == "" || GameManager.nameP2 == "")
        {
            // if one of the input fields is empty
        }
        else
        {
            _controleMenu.SetActive(true);
            _nameMenu.SetActive(false);
        }
    }
}
