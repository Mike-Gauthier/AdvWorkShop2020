using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScirpt : MonoBehaviour
{
    private bool inside;
    private GameObject cage;


    private void Start()
    {
        cage = this.gameObject.GetComponentInParent<GameObject>();
    }

    public void Update()
    {
        if (inside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Release the trapped person
                Debug.Log("Person freed");
                Destroy(cage);
            }
        }      
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = false;
        }
    }


}
