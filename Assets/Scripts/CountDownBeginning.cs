using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownBeginning : MonoBehaviour
{
    public GameObject onePage;
    public GameObject twoPage;
    public GameObject threePage;

    void Start()
    {
        onePage.SetActive(false);
        twoPage.SetActive(false);
        threePage.SetActive(false);
    }
    
    
    public void ShowCountdown()
    {
        onePage.SetActive(true);
        CountingDown();
        twoPage.SetActive(true);
        CountingDown();
        threePage.SetActive(true);
        CountingDown();
    }
    
    IEnumerator CountingDown()
    {
        
        yield return new WaitForSeconds(1);
        
    }
}
