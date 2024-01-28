using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitcomIntroController : MonoBehaviour
{
    public AK.Wwise.Event sitcomIntroEvent;
    public Animator animation;
    
    public NPCDialogueController margaret;
    
    

    // Start is called before the first frame update
    private void Start()
    {
        sitcomIntroEvent.Post(gameObject);

        StartCoroutine(StartCoroutine());
        
        margaret = GameObject.Find("Margaret").GetComponent<NPCDialogueController>();
    }
    
    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        animation.Play("do");
        yield return new WaitForSeconds(6.0f);
        
        margaret.StartDialogue();
    }
}
