using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public TMP_Text counter;
    public int secondsLeft;
    private bool takingAway = false;
    public GameManager GameManager;
    
    void Start()
    {
        counter.SetText(secondsLeft.ToString());
        GameManager = GameManager.Instance;
    }


    void Update()
    {
        // counting down to 0
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(CountingDown());
        }
        // no time left anymore
        if (secondsLeft == 0)
        {
            GameManager.OnTimeIsOver();

        }
    }

    IEnumerator CountingDown()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        counter.SetText(secondsLeft.ToString());
        takingAway = false;
    }

    public void TimeOver()
    {
         if (secondsLeft == 0)
         {
            Debug.Log("yes");
         }
    }
    
   
}
