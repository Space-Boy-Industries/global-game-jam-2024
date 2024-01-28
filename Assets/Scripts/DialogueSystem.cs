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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
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
    
    /**
     * quick hack to get the dialogue to render correctly
     * (this is really expensive, but it's the only way I could get it to work)
     */
    private void ForceLayoutGroupUpdate()
    {
        var layoutGroups = GetComponentsInChildren<LayoutGroup>();
        foreach (var layoutGroup in layoutGroups)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
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
        // wait 1 frame so that no input is accidentally registered
        yield return null;
        
        while (story.canContinue || story.currentChoices.Count > 0)
        {
            if (story.canContinue)
            {
                // if we have text to display, display it
                
                var text = story.Continue();
                DisplayLine(text);

                // HACK: wait 1 frame so that the text is rendered before we force the layout group to update
                yield return null;
                ForceLayoutGroupUpdate();
                
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // wait for input to continue

            } else if (story.currentChoices.Count > 0) {
                // if there is a choice in the dialogue, display choices
                
                var choices = story.currentChoices;
                var choice = -1;
                var choiceObject = DisplayChoices(choices, (int choiceIndex) =>
                {
                    choice = choiceIndex;
                });
                
                // HACK: wait 1 frame so that the text is rendered before we force the layout group to update
                yield return null;
                ForceLayoutGroupUpdate();
            
                yield return new WaitUntil(() => choice != -1); // wait for a choice to be made
                DestroyChoice(choiceObject);
                story.ChooseChoiceIndex(choice);
            }

            // if current dialogue is tagged as "close", close the dialogue. (this allows us to keep the whole dialogue for one npc in one file but then just close it when they are done talking in their current state)
            if (story.currentTags.Contains("close"))
            {
                break;
            }
            
            // invoke dialogue event (for triggering animations and sounds and all that stuff)
            OnDialogueEvent?.Invoke(new DialogueEventContext(story.currentTags.ToArray()));
            
            yield return null;
        }
        
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // wait for input to close
        CloseDialogue(true);
    }
}