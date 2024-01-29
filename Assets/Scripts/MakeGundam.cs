using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGundam : MonoBehaviour
{
    public GameObject GundamModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GundamModel.activeSelf && GlobalStateSystem.Instance.GlobalState.ContainsKey("built_gundam") && (bool) GlobalStateSystem.Instance.GlobalState["built_gundam"])
        {
            GundamModel.SetActive(true);
        }
    }
}
