using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject sample;
    public Text interactPopUp;
    void Start()
    {
        sample = GameObject.FindGameObjectWithTag("sample");
    }
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Sample());
            interactPopUp.text = "Press E interact";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactPopUp.text = "";
    }

    IEnumerator Sample()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(sample);
    }
}
