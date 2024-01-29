using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeGameObject : MonoBehaviour
{
    public UnityEvent OnClickEvent;

    public Collider Collider { get; private set; }

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CoffeeGameController.IsMinigameActive)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Check if object clicked
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        OnClickEvent.Invoke();
                    }
                }
            }
        }
    }
}
