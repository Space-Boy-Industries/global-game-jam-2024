using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class HideRoof : MonoBehaviour
{
    [SerializeField]
    private bool disableOnStart = false;
    
    [SerializeField]
    private GameObject[] disableWhenInside;

    private void Start()
    {
        foreach (var obj in disableWhenInside)
        {
            obj.SetActive(!disableOnStart);
        }
    }

    private IEnumerator FadeToHate()
    {
        float opacity = 1f;
        var meshes = disableWhenInside.ToList()
            .ConvertAll((obj) => obj.GetComponent<MeshRenderer>())
            .FindAll((mesh) => mesh != null);
        
        yield return new WaitWhile(() =>
        {
            foreach (var mesh in meshes)
            {
                var color = mesh.material.color;
                color.a = opacity;
                mesh.material.color = color;
            }
            
            opacity -= Time.deltaTime;
            
            return opacity > 0f;
        });
        
        foreach (var obj in disableWhenInside)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (var obj in disableWhenInside)
        {
            obj.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        foreach (var obj in disableWhenInside)
        {
            obj.SetActive(true);
        }
    }
}
