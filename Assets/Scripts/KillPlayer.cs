using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject currentCheckpoint;
    [SerializeField]
    private GameObject player;
    //Trigger to check for 
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        if (collider.name == "Player")
        {
            player.transform.position = currentCheckpoint.transform.position;
        }
    }

}
