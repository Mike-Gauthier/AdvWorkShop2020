using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    public GameObject transparentObject;
    public GameObject realObject;
    public BaseBehavior bScript;
    private bool built;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(built==false)
            {
                transparentObject.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.E) && built == false && this.tag == "oven" && bScript.baseWoodCount >= 10 && bScript.baseStoneCount >= 10)
            {
                bScript.baseWoodCount = bScript.baseWoodCount - 10;
                bScript.baseStoneCount = bScript.baseStoneCount - 10;
                StartCoroutine(Build());
            }
            if (Input.GetKeyDown(KeyCode.E) && built == false && this.tag == "bridge" && bScript.baseWoodCount >= 10)
            {
                bScript.baseWoodCount = bScript.baseWoodCount - 10;
                StartCoroutine(Build());
            }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
            transparentObject.SetActive(false);
    }

    IEnumerator Build()
    {
        yield return new WaitForSeconds(.01f);
        transparentObject.SetActive(false);
        realObject.SetActive(true);
        built = true;
    }
   
}
