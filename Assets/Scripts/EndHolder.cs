using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHolder : MonoBehaviour
{

    // This script has one simple purpose; Wait for dialogue to be over, then start this objects child.

    [SerializeField] private DialogueManager dialoguemanager;
    // The child object to be activated
    [SerializeField] private GameObject thing;
    // Bool to see if this is finished
    [SerializeField] private bool done;


    // Update is called once per frame
    void Update()
    {
        if (dialoguemanager.dialoguePlaying == false && done == false)
        {
            thing.SetActive(true);
            done = true;
        }
    }
}
