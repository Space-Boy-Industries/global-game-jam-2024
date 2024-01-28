using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalStateUpdateContext
{
    public string VariableName { get; private set; }
    public object Value { get; private set; }
    public Dictionary<string, object> State { get; private set; }
    
    public GlobalStateUpdateContext(string variableName, object value, Dictionary<string, object> state)
    {
        VariableName = variableName;
        Value = value;
        State = state;
    }
}

public class GlobalStateSystem : MonoBehaviour
{
    public static GlobalStateSystem Instance { get; private set; }
    public Dictionary<string, object> GlobalState { get; private set; }
    public UnityEvent<GlobalStateUpdateContext> OnGlobalStateUpdate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GlobalState = new Dictionary<string, object>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetFlag(string variableName, object value)
    {
        GlobalState[variableName] = value;
        OnGlobalStateUpdate?.Invoke(new GlobalStateUpdateContext(variableName, value, GlobalState));
    }
    
    public void DebugPrintState()
    {
        foreach (var key in GlobalState.Keys)
        {
            Debug.Log($"{key}: {GlobalState[key]}");
        }
    }
}