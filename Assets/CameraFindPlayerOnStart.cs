using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFindPlayerOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var player = GameObject.FindWithTag("Player")?.transform;
        if (player == null) return;
        GetComponent<CinemachineVirtualCamera>().Follow = player;
    }
}
