using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionSystem : MonoBehaviour
{
    
    [SerializeField] private Fader fader;
    public static SceneTransitionSystem Instance { get; private set; }
    public bool IsTransitioning { get; private set; } = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(fader.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
    
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        IsTransitioning = true;
        fader.fadeDirection = FadeDirection.OUT;
        fader.Fade();

        yield return new WaitForSeconds(fader.fadeTime);
        
        Debug.Log("BEFORE LOAD SCENE");
        GlobalStateSystem.Instance.DebugPrintState();

        fader.fadeDirection = FadeDirection.IN;
        SceneManager.LoadScene(sceneName);
        fader.Fade();
        
        Debug.Log("AFTER LOAD SCENE");
        GlobalStateSystem.Instance.DebugPrintState();
        
        yield return new WaitForSeconds(fader.fadeTime);

        IsTransitioning = false;
    }
}
