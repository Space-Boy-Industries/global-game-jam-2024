using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class EndingHandler : MonoBehaviour
{
    public AK.Wwise.Event endingTheme;
    public TextAsset endingText;
    private Story endingStory;

    public static bool IsEnd;
    
    private void Start()
    {
        if (endingText != null)
        {
            endingStory = new Story(endingText.text);
            GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener((context) =>
            {
                if (context.VariableName == "talked_about_life" && context.Value.ToString().ToLower() == "true")
                {
                    IsEnd = true;
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

        yield return new WaitUntil(() => !DialogueSystem.Instance.IsOpen);
        yield return new WaitForSeconds(3f);
        Debug.Log("Exiting");
        Application.Quit();
    }
}
