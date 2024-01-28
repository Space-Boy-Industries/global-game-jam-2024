using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CoffeeGameController : MonoBehaviour
{
    public GameObject PlayerModel;
    public GameObject Interactable;
    public GameObject CoffeeDispenseObject;
    public CoffeeGameObject[] CoffeeGameObjects;

    public bool IsMinigameActive { get; private set; }

    private Collider collider;
    private CinemachineVirtualCamera virtualCamera;

    // Minigame state vars
    private bool coffeeDispensing;
    private bool coffeeDispensed;
    private bool milkAdded;
    private int sugarAdded;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMinigameActive) {

            if (Input.GetButtonDown("Cancel")) {
                QuitMinigame();
            }
        }
    }

    public void StartMinigame()
    {
        GlobalStateSystem.Instance.SetFlag("has_coffee", false);
        foreach (var obj in CoffeeGameObjects)
        {
            obj.Collider.enabled = true;
        }

        collider.enabled = false;
        Interactable.SetActive(false);
        PlayerModel.SetActive(false);
        virtualCamera.enabled = true;
        IsMinigameActive = true;

        coffeeDispensing = false;
        coffeeDispensed = false;
        milkAdded = false;
        sugarAdded = 0;
    }

    public void QuitMinigame()
    {
        foreach (var obj in CoffeeGameObjects)
        {
            obj.Collider.enabled = false;
        }

        collider.enabled = true;
        Interactable.SetActive(true);
        PlayerModel.SetActive(true);
        virtualCamera.enabled = false;
        IsMinigameActive = false;
    }

    private IEnumerator CoffeeDispenseAnimation()
    {
        coffeeDispensing = true;
        var t = 0f;
        var startPos = CoffeeDispenseObject.transform.localPosition;
        var startScale = CoffeeDispenseObject.transform.localScale;
        yield return new WaitWhile(() => {
            t += Time.deltaTime * 0.5f;
            CoffeeDispenseObject.transform.localScale = new Vector3(startScale.x, t, startScale.z);
            CoffeeDispenseObject.transform.localPosition = new Vector3(startPos.x, startPos.y - t, startPos.z);

            return t < 1f;
        });

        CoffeeDispenseObject.transform.localPosition = startPos;
        CoffeeDispenseObject.transform.localScale = startScale;
        coffeeDispensing = false;
        coffeeDispensed = true;
    }

    public void DispenseCoffee()
    {
        if (!coffeeDispensing && !coffeeDispensed)
        {
            StartCoroutine(CoffeeDispenseAnimation());
        }
    }

    public void AddMilk()
    {
        if (coffeeDispensed) {
            milkAdded = true;
        }
    }

    public void AddSugar()
    {
        if (coffeeDispensed) {
            sugarAdded++;
        }
    }

    public void ServeCoffee()
    {
        Debug.Log("Coffee dispensed: " + coffeeDispensed + ", milk added: " + milkAdded + ", sugar added: " + sugarAdded);
        if (coffeeDispensed) {
            GlobalStateSystem.Instance.SetFlag("has_coffee", true);
            GlobalStateSystem.Instance.SetFlag("coffee_correct", coffeeDispensed && milkAdded && sugarAdded == 2);
            QuitMinigame();
        }
    }
}
