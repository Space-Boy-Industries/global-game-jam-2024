using UnityEngine;

public class EventBasedObjectSwapper : MonoBehaviour
{
    [SerializeField] public string variableName;
    [SerializeField] public bool enableValueFilter;
    [SerializeField] public string value;
    [SerializeField] public GameObject[] prefabToSwapIn;
    
    private void OnGlobalStateUpdate(GlobalStateUpdateContext context)
    {
        if (context.VariableName != variableName) return;
        if (enableValueFilter)
        {
            if (context.Value.ToString().ToLower() != value) return;
        }
        
        foreach (var prefab in prefabToSwapIn)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }
    
    private void Start()
    {
        GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener(OnGlobalStateUpdate);
    }
    
    private void OnDestroy()
    {
        GlobalStateSystem.Instance.OnGlobalStateUpdate.RemoveListener(OnGlobalStateUpdate);
    }
}