using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class EndingHandler : MonoBehaviour
{
    public AK.Wwise.Event endingTheme;
    public TextAsset endingText;
    private Story endingStory;
    
    private void Start()
    {
        if (endingText != null)
        {
            endingStory = new Story(endingText.text);
            GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener((context) =>
            {
                if (context.VariableName == "talked_about_life" && context.Value.ToString().ToLower() == "true")
                {
                    StartCoroutine(EndingCoroutine());
                }
            });
        }
    }
    
    private IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(6.0f);
        endingTheme.Post(gameObject);
        yield return new WaitForSeconds(1.0f);
        if (DialogueSystem.Instance.IsOpen)
        {
            DialogueSystem.Instance.CloseDialogue();
        }
        DialogueSystem.Instance.SetStory(endingStory);
        DialogueSystem.Instance.OpenDialogue();
    }
}
