using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] int pickUpValue = 1;

    bool hasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasCollected)
        {
            // Update player score with pick up value
            hasCollected = true;
            FindObjectOfType<ScoreKeeper>().ModifyScore(pickUpValue);
            AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
            
            Destroy(gameObject);
        }
    }
}
