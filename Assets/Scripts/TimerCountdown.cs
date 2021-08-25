using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public TMP_Text counter;
    public int secondsLeft = 300;
    public bool takingAway = false;
    
    
    void Start()
    {
        counter.SetText(secondsLeft.ToString());
    }

// Update is called once per frame
    void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(CountingDown());
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
}
