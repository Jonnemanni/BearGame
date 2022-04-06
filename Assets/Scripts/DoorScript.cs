using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{   
    // Telling this door which one it leads to.
    [SerializeField] private GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method that is called by a player wanting to walk through a door. Receives the player/walker as a game object and then moves that player to where this doors exit is.
    public void WalkThrough(GameObject walker)
    {
        Debug.Log("Tried to walk through.");
        walker.transform.position = exit.transform.position;
    }
}
