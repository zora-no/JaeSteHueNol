using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // when pressing the button game will start
    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

   
}
