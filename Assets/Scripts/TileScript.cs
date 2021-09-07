using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{


    [SerializeField] private float effectDuration = 5f;
    [SerializeField] private int effectType = 3;
    private bool effectIsActive = false;
    GameObject affectedPlayerObject;
    GameObject shootingPlayerObject;
    GameObject player1;
    GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // If wall hit by any ball
        if (other.tag == "Ball1" || other.tag == "Ball2")
        {   
            // checks if no tile effect is active
            if (effectIsActive == false)
            {   
                // check to affect the correct player
                if (other.tag == "Ball1")
                {
                    affectedPlayerObject = player2;
                    shootingPlayerObject = player1;
                    effectType = shootingPlayerObject.GetComponent<PlayerMovementScript>().tileEffectType;
                }
                else
                {
                    affectedPlayerObject = player1;
                    shootingPlayerObject = player2;
                    effectType = shootingPlayerObject.GetComponent<PlayerMovementScript>().tileEffectType;
                }
                
                // activate effect
                switch (effectType)
                {   
                    // Movement inhibitor powerup active
                    case 0:
                        gameObject.transform.Find("MovementInhibitor").gameObject.SetActive(true);
                        effectIsActive = true;
                        shootingPlayerObject.GetComponent<PlayerMovementScript>().ResetTileEffectType();
                        StartCoroutine(DeactivateEffect(effectType));
                        break;
                    // Slow field powerup active
                    case 1:
                        gameObject.transform.Find("SlowField").gameObject.SetActive(true);
                        effectIsActive = true;
                        shootingPlayerObject.GetComponent<PlayerMovementScript>().ResetTileEffectType();
                        StartCoroutine(DeactivateEffect(effectType));
                        break;
                    // Vision impairment powerup active
                    case 2:
                        gameObject.transform.Find("CloudedViewField").gameObject.SetActive(true);
                        effectIsActive = true;
                        shootingPlayerObject.GetComponent<PlayerMovementScript>().ResetTileEffectType();
                        StartCoroutine(DeactivateEffect(effectType));
                        break;
                    // No tile powerup active
                    case 3:
                        break;
                }
            }
        }
    }
    // Deactivates powerup effect
    IEnumerator DeactivateEffect(int type)
    {
        yield return new WaitForSeconds(effectDuration);
        switch (effectType)
        {
            case 0:
                gameObject.transform.Find("MovementInhibitor").gameObject.SetActive(false);
                effectIsActive = false;
                break;
            case 1:
                Debug.Log("Deactivating slow...");
                gameObject.transform.Find("SlowField").gameObject.SetActive(false);
                effectIsActive = false;
                affectedPlayerObject.GetComponent<PlayerMovementScript>().ResetMovespeed();
                break;
            case 2:
                gameObject.transform.Find("CloudedViewField").gameObject.SetActive(false);
                effectIsActive = false;
                affectedPlayerObject.GetComponent<PlayerMovementScript>().ResetVision();
                break;
        }
    }
}
