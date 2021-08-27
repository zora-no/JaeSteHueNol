using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{


    [SerializeField] private float effectDuration = 5f;
    [SerializeField] private int effectType = 1;
    private bool effectIsActive = false;
    private int affectedPlayer;
    GameObject affectedPlayerObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ball1" || other.tag == "Ball2")
        {
            if (effectIsActive == false)
            {
                if (other.tag == "Ball1")
                {
                    affectedPlayer = 2;
                    affectedPlayerObject = GameObject.Find("Player2");
                }
                else
                {
                    affectedPlayer = 1;
                    affectedPlayerObject = GameObject.Find("Player1");
                }
                Debug.Log("hit!");
                switch (effectType)
                {
                    case 0:
                        transform.GetChild(effectType).gameObject.SetActive(true);
                        break;
                    case 1:
                        transform.GetChild(effectType).gameObject.SetActive(true);
                        break;
                }
                effectIsActive = true;
                StartCoroutine(DeactivateEffect(effectType));
            }
        }
    }

    IEnumerator DeactivateEffect(int type)
    {
        yield return new WaitForSeconds(effectDuration);
        switch (effectType)
        {
            case 0:
                transform.GetChild(effectType).gameObject.SetActive(false);
                effectIsActive = false;
                break;
            case 1:
                transform.GetChild(effectType).gameObject.SetActive(false);
                effectIsActive = false;
                affectedPlayerObject.GetComponent<PlayerMovementScript>().ResetMovespeed();
                break;
        }
    }
}
