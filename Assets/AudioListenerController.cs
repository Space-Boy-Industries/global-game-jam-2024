using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
    public GameObject PlayerModel;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (CoffeeGameController.IsMinigameActive)
        {
            transform.position = mainCamera.transform.position;
        }
        else
        {
            transform.position = PlayerModel.transform.position;
        }

        transform.rotation = mainCamera.transform.rotation;
    }
}
