using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Visual cue to tell that we are in talking range.
    [SerializeField] private GameObject visualCue;
    
    // the inkJSON file to be passed to this.
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
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

    // Update that will check if the player is in range, pressing the talk button, and not in dialogue.
    // Then there will be talk.
    private void Update()
    {
        if (Input.GetButtonDown("Talk") && playerInRange && !DialogueManager.GetInstance().dialoguePlaying)
        {
            DialogueManager.GetInstance().EnterDialogue(inkJSON);
        }        
    }
}
