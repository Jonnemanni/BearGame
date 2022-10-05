using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    // UI Stuff.
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;

    // EndHolder object, activated so that it can then wait for dialogue to end and activate the EndCutscene.
    [SerializeField] private GameObject EndHolder;

    // Choices UI
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    // Variable to hold the dialogue currently being played through.
    private Story currentStory;

    // Boolean to check if there is dialogue on, and make it read only to outside script.
    public bool dialoguePlaying { get; private set;}

    // Setting up a singleton class and initializing it in the awake method.
    private static DialogueManager instance;

    // Constants relating to tags in our INK files
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string END_TAG = "end";
    private const string POINTS_TAG = "points";
    // Getting the scoreSO
    [SerializeField] private ScoreTracker scoreSO;


    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found one or more dialogue managers already in scene.");
        }
        instance = this;
    }

    // Setting everything ready.
    private void Start() 
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);

        // Get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    
    // Using Update to go through the dialogue logic.
    private void Update()
    {
        // If no dialogue, return
        if (!dialoguePlaying)
        {
            return;
        }

        // If the player presses the button to progress, then do so.
        if (Input.GetButtonDown("Talk"))
        {
            ContinueStory();
        }
    }

    // A method that returns the current instance of this.
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    // A method for exiting dialogue.
    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.01f);

        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // Entering and doing dialogue.
    public void EnterDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    // Method that simply continues the story, since it's done at multiple places.
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // Set text for current dialogue line.
            dialogueText.text = currentStory.Continue();
            // Display choices.
            DisplayChoices();
            // Handling tags.
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    // Method for handling the speaker, portrait, end, and score tags.
    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it.
        foreach(string tag in currentTags)
        {
            // Parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Could not parse tag, not correct length. " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag.
            switch(tagKey)
            {
                case SPEAKER_TAG:
                    Debug.Log("Speaker: " + tagValue);
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    Debug.Log("Portrait: " + tagValue);
                    portraitAnimator.Play(tagValue);
                    break;
                case END_TAG:
                    Debug.Log("End: " + tagValue);
                    EndHolder.SetActive(true);
                    break;
                case POINTS_TAG:
                    Debug.Log("Points: " + tagValue);
                    scoreSO.Score += int.Parse(tagValue);
                    break;
                default:
                    Debug.Log("Tag came in but wasn't handled." + tagKey);
                    break;
            }
        }
    }

    // Method for dealing with the choices in the UI
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // error checking in case we end up with more choices than the UI can support.
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More coices than the UI supports.");
        }

        int index = 0;

        // Enable and initialize our choices.
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // Get the remaining choices in the UI and disable them.
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
