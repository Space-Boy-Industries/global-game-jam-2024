using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // singleton
    public static DialogueSystem Instance { get; private set; }
    public UnityEvent OnDialogueOpen;
    public UnityEvent OnDialogueClose;
    public UnityEvent<DialogueEventContext> OnDialogueEvent;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There can only be one DialogueSystem.");
            DestroyImmediate(this);
        }
    }

    [SerializeField] private RectTransform dialogueContent;
    [SerializeField] private GameObject dialogueLinePrefab;
    [SerializeField] private GameObject choiceListPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private GameObject canvas;
    
    private Story story;
    private bool _isOpen;
    public bool isOpen => _isOpen;

    private void Start()
    {
        // hide the canvas if open
        canvas.SetActive(false);
        
        GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener((context) =>
        {
            if (story == null) return;
            story.variablesState[context.VariableName] = context.Value;   
        });
    }

    public void ClearDialogue()
    {
        // delete all children
        foreach (Transform child in dialogueContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetStory(Story input)
    {
        story = input;
    }
    
    public void OpenDialogue(bool clear = true)
    {
        if (_isOpen)
        {
            throw new Exception("Dialogue is already open.");
        }
        
        if (!story)
        {
            throw new Exception("No story has been set.");
        }
        
        if (clear)
        {
            ClearDialogue();
        }
        
        // set current properties when opening
        foreach (var variable in GlobalStateSystem.Instance.GlobalState)
        {
            story.variablesState[variable.Key] = variable.Value;
        }
        
        canvas.SetActive(true);
        _isOpen = true;
        OnDialogueOpen?.Invoke();
        StartCoroutine(RunStory());
    }

    public void CloseDialogue(bool clear = true)
    {
        if (!_isOpen)
        {
            throw new Exception("Dialogue is not open.");
        }
        
        _isOpen = false;
        canvas.SetActive(false);
        
        OnDialogueClose?.Invoke();
        
        if (clear)
        {
            ClearDialogue();
        }
    }
    
    private void DisplayLine(string text)
    {
        // create a new line
        var line = Instantiate(dialogueLinePrefab, dialogueContent);
        // set the text
        line.GetComponent<TMP_Text>().SetText(text);
    }
    
    private GameObject DisplayChoices(List<Choice> choices, Action<int> callback)
    {
        var choiceList = Instantiate(choiceListPrefab, dialogueContent);
        foreach (var choice in choices)
        {
            var choiceObject = Instantiate(choicePrefab, choiceList.transform);
            choiceObject.GetComponentInChildren<TMP_Text>().SetText(choice.text);
            choiceObject.GetComponent<Button>().onClick.AddListener(() => callback(choice.index));
        }

        return choiceList;
    }

    private void DestroyChoice(GameObject choiceObj)
    {
        Destroy(choiceObj);
    }

    // coroutine to run the story
    private IEnumerator RunStory()
    {
        while (story.canContinue || story.currentChoices.Count > 0)
        {
            Debug.Log(story.currentFlowName);
            if (story.canContinue)
            {
                var text = story.Continue();
                DisplayLine(text);
                
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // wait for input to continue

            } else if (story.currentChoices.Count > 0) {
                // display choices
                var choices = story.currentChoices;
                var choice = -1;
                var choiceObject = DisplayChoices(choices, (int choiceIndex) =>
                {
                    choice = choiceIndex;
                });
            
                yield return new WaitUntil(() => choice != -1); // wait for a choice to be made
                DestroyChoice(choiceObject);
                story.ChooseChoiceIndex(choice);
            }

            // if current story tag contains "close", close the dialogue for now
            if (story.currentTags.Contains("close"))
            {
                break;
            }
            
            OnDialogueEvent?.Invoke(new DialogueEventContext(story.currentTags.ToArray()));
            
            yield return null;
        }
        
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // wait for input to close
        CloseDialogue(true);
    }
}