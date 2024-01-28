using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SoundMaterialTrigger : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Switch soundMaterialSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        var wiseObj = other.gameObject.GetComponentInChildren<PostWwiseEvent>()?.gameObject;
        if (!wiseObj) return;
            
        soundMaterialSwitch.SetValue(wiseObj);
    }
}