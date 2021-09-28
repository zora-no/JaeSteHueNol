using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownBeginning : MonoBehaviour
{
    [SerializeField]
    private GameObject onePage;
    [SerializeField]
    private GameObject twoPage;
    [SerializeField]
    private GameObject threePage;
    [SerializeField]
    private GameObject goPage;

    void Start()
    {
        SetPageState(PageState.None);
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("counting inst");
            ShowCountdown();
        }
            
    }
    enum PageState
        // the five page states
    {
        None,
        Three,
        Two,
        One,
        Go
        
    }
    
    public void ShowCountdown()
    {
        StartCoroutine(CountingDown());
        Debug.Log("counting down function");
        //SetPageState(PageState.Three);
    }
    
    
    IEnumerator CountingDown()
    {
        Debug.Log("counting down Coroutine");
        yield return new WaitForSeconds(1);
        SetPageState(PageState.Three);
        yield return new WaitForSeconds(1);
        SetPageState(PageState.Two);
        yield return new WaitForSeconds(1);
        SetPageState(PageState.One);
        yield return new WaitForSeconds(1);
        SetPageState(PageState.Go);
        yield return new WaitForSeconds(2);
        SetPageState(PageState.None);
        
        
    }
    
    void SetPageState(PageState state)
        // activate needed page and deactivate all others
    {
        switch (state)
        {
            case PageState.None:
                threePage.SetActive(false);
                twoPage.SetActive(false);
                onePage.SetActive(false);
                goPage.SetActive(false);
                break;
            case PageState.Three:
                threePage.SetActive(true);
                twoPage.SetActive(false);
                onePage.SetActive(false);
                goPage.SetActive(false);
                break;
            case PageState.Two:
                threePage.SetActive(false);
                twoPage.SetActive(true);
                onePage.SetActive(false);
                goPage.SetActive(false);
                break;
            case PageState.One:
                threePage.SetActive(false);
                twoPage.SetActive(false);
                onePage.SetActive(true);
                goPage.SetActive(false);
                break;
            case PageState.Go:
                threePage.SetActive(false);
                twoPage.SetActive(false);
                onePage.SetActive(false);
                goPage.SetActive(true);
                break;
            
           
        }
    }
}
