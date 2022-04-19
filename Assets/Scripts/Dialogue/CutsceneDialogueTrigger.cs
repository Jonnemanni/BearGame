using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    
    // the inkJSON file to be passed to this.
    [SerializeField] private TextAsset inkJSON;

    // Starting the script once a cutscene ends.
    private void OnEnable()
    {
        Debug.Log("Cutscene text was enabled.");
        DialogueManager.GetInstance().EnterDialogue(inkJSON);
    }
}
