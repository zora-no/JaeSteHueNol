using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTilePowerups : MonoBehaviour
{

    public Sprite InhibitorImg;
    public Sprite SlowImg;
    public Sprite CloudedImg;
    public Sprite NoPowerupImg;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "TilePowerup1")
        {

        }
        else if (this.name == "TilePowerup2")
            {

        }
        else
        {
            Debug.LogError("UI Tile powerup couldn't identify player!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchImage(int img)
    {
        switch (img)
        {
            case 0:
                gameObject.GetComponent<Image>().sprite = InhibitorImg;
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = SlowImg;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = CloudedImg;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = NoPowerupImg;
                break;
            default:
                Debug.LogError(" UI switch image couldnt find image!");
                break;
        }
    }
}
