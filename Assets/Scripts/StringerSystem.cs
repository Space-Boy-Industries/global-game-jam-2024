using UnityEngine;


public class StringerSystem : MonoBehaviour
{
    public static StringerSystem Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    
    public AK.Wwise.Event alTheme;
    public AK.Wwise.Event wifeTheme;
    public AK.Wwise.Event billyTheme;
    public AK.Wwise.Event endingTheme;

    // Start is called before the first frame update
    private void Start()
    {
        GlobalStateSystem.Instance.OnGlobalStateUpdate.AddListener((ctx) =>
        {
            if (ctx.Value.ToString().ToLower().Trim() != "true")
                return;
            
            switch (ctx.VariableName)
            {
                case "cleaned_table":
                case "made_coffee":
                    wifeTheme.Post(gameObject);
                    break;
                case "did_homework":
                case "built_gundam":
                    billyTheme.Post(gameObject);
                    break;
                case "gave_life_advice":
                    Debug.Log("al theme");
                    alTheme.Post(gameObject);
                    break;
                case "talked_about_life":
                    endingTheme.Post(gameObject);
                    break;
            }
        });
    }
}
