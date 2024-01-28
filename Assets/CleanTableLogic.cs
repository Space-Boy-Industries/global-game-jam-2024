using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTableLogic : MonoBehaviour
{
    public AK.Wwise.Event CleanTableSound;

    public void CleanTable()
    {
        StartCoroutine(CleanTableCoroutine());
    }
    
    private IEnumerator CleanTableCoroutine()
    {
        CleanTableSound.Post(gameObject);
        yield return new WaitForSeconds(1.5f);
        GlobalStateSystem.Instance.SetFlag("cleaned_table", true);
        Destroy(gameObject);
        
    }
}
