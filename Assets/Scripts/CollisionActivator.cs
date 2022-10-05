using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActivator : MonoBehaviour
{
    // Visual cue to tell that we are in activation range.
    [SerializeField] private GameObject visualCue;

    // The object we activate on trigger.
    [SerializeField] private GameObject activeObject;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // Update that will check if the player is in range, pressing the talk button, and not in dialogue.
    // Then the cutscene will begin and the trigger will be disabled.
    private void Update()
    {
        if (Input.GetButtonDown("Talk") && playerInRange && !DialogueManager.GetInstance().dialoguePlaying)
        {
            activeObject.SetActive(true);
            gameObject.SetActive(false);
        }        
    }

    // Trigger on stay that will check if the player is in range of the NPC being talked to.
    // Thus, displaying the talk indicator and allowing talk to happen.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    // On trigger exit that turns off the talk indicator.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }        
    }
}
