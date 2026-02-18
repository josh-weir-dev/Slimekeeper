using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // declaring variables needed in the script
    public string Tagtarget = "Player";

    public Collider2D col;
    // making a list for the detected objects in the range of the enemy
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D collider)
    {
        // if the detection zone collides with another collider object this checks if it collides with s an object with the player tag
        if (collider.gameObject.tag == Tagtarget)
        {
            // here the player's collider is being added to the list of detected objects
            detectedObjs.Add(collider);
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // this method checks that the player collider leaves the detection zone and if it does it is removed from the detected objects list
        if (collider.gameObject.tag == Tagtarget)
        {
            detectedObjs.Remove(collider);
        }
        
    }

}
