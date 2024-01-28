using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent action;

    private float startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        var scale = startScale + Mathf.Sin(Time.time * 5) * 0.03f;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Interact() {
        action?.Invoke();
    }
}
