using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event EventTrigger;
    // Start is called before the first frame update
    public void PlayFootstepSound()
    {
        EventTrigger.Post(gameObject);
    }

}
