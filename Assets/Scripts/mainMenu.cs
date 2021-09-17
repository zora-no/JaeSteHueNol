using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField]
    
    GameManager GameManager;
    public GameObject _mainMenu;
    public GameObject _descriptionMenu;
    public GameObject _scoreHistoryMenu;
    public GameObject _nameMenu;
    public GameObject _controleMenu;
    
    void Awake()
    {
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
        
        Debug.Log("Play!");
        
    }
    
    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
