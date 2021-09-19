using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    GameManager GameManager;

    [SerializeField]
    public GameObject _mainMenu;
    public GameObject _descriptionMenu;
    public GameObject _scoreHistoryMenu;
    public GameObject _nameMenu;
    public GameObject _controleMenu;
    
    void Awake()
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
            ;
        }
        else
        {
            _controleMenu.SetActive(true);
            _nameMenu.SetActive(false);
        }
    }
}
