using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{   
    // Telling this door which scene it leads to by giving it the index. See Loader.cs for the list.
    [SerializeField] private string scene;
    // Enumerated list of possible scenes.
    // Telling this door which other door in the scene it leads to.
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
    // Prioritizes sending them to another scene.
    public void WalkThrough(GameObject walker)
    {
        if (scene != null) {
            Debug.Log("Tried to walk to different scene.");
            SceneManager.LoadScene(scene);
        }
        else if (exit != null) {
            Debug.Log("Tried to walk in scene.");
            walker.transform.position = exit.transform.position;
        } else {
            Debug.Log("No scene or exit set.");
        }
    }
}
