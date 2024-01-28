using System;
using AK.Wwise;
using Ink.Runtime;
using UnityEngine;
using Event = AK.Wwise.Event;

public class NPCDialogueController : MonoBehaviour
{
    [SerializeField]
    private TextAsset storyAsset;
    public Story story { get; private set; }

    private void Start()
    {
        story = new Story(storyAsset.text);
        
        story.BindExternalFunction ("setFlag", (string variableName, object value) =>
        {
            Debug.Log($"Setting flag {variableName} to {value}");
            GlobalStateSystem.Instance.SetFlag(variableName, value);
        });
        
        story.BindExternalFunction("loadScene", (string sceneName) =>
        {
            if (DialogueSystem.Instance.isOpen)
            {
                DialogueSystem.Instance.CloseDialogue();
            }
            SceneTransitionSystem.Instance.LoadScene(sceneName);
        });
        
        story.BindExternalFunction("playSound", (string soundName) => throw new NotImplementedException());
        story.BindExternalFunction("playAnimation", (string animationName, bool persist) => throw new NotImplementedException());
    }

    [ContextMenu("Start Dialogue")]
    public void StartDialogue()
    {
        DialogueSystem.Instance.SetStory(story);
        DialogueSystem.Instance.OpenDialogue(true);
    }
}
