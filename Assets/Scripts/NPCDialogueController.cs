using Ink.Runtime;
using UnityEngine;

public class NPCDialogueController : MonoBehaviour
{
    [SerializeField]
    private TextAsset storyAsset;
    public Story story { get; private set; }

    private void Start()
    {
        story = new Story(storyAsset.text);
    }
    private void OnMouseDown()
    {
        StartDialogue();
    }

    [ContextMenu("Start Dialogue")]
    public void StartDialogue()
    {
        DialogueSystem.Instance.SetStory(story);
        DialogueSystem.Instance.OpenDialogue(true);
    }
}
