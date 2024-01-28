using System;
using UnityEngine;


[Serializable]
public class SoundEvent
{
    public string name;
    public AK.Wwise.Event Event;
}

public class GlobalSoundSystem : MonoBehaviour
{
    public static GlobalSoundSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public SoundEvent[] soundEvents;
    public AK.Wwise.Switch[] laughSwitch;
    private int laughIndex = 0;

    private void Start()
    {
        GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener((ctx) =>
        {
            if (ctx.Value.ToString() != "true")
                return;

            switch (ctx.VariableName)
            {
                case "in_sitcom":
                    laughIndex = 0;
                    break;
                case "cleaned_table":
                case "did_homework":
                case "gave_life_advice":
                    if (laughIndex < 3)
                        laughIndex++;
                    break;
                case "made_coffee":
                case "built_gundam":
                case "talked_about_life":
                    if (laughIndex < laughSwitch.Length)
                    {
                        laughIndex++;
                    }

                    break;
            }
            
            laughIndex = Mathf.Clamp(laughIndex, 0, laughSwitch.Length - 1);
            laughSwitch[laughIndex].SetValue(gameObject);
        });
    }

    public void PlaySound(string soundName)
    {
        var soundEvent = Array.Find(soundEvents, e => e.name == soundName);
        if (soundEvent == null)
        {
            Debug.LogError($"Could not find sound event with name {soundName}");
            return;
        }

        soundEvent.Event.Post(gameObject);
    }
}