using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OvenBehavior : MonoBehaviour
{
    public BaseBehavior bScript;
    public GameObject brick;
    public GameObject holder;
    public bool cooking;
    public bool coolDown;
    //public TextMeshProUGUI popUp;

    private void Start()
    {
        cooking = false;
        coolDown = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("staying");
            if (cooking == false && bScript.baseMudCount >= 1 && bScript.baseWoodCount >= 1)
            {
                //popUp.text = "Press E to turn oven on.";
            }
            if (cooking == true)
            {
                //popUp.text = "Press E to turn oven off.";
            }
            if (Input.GetKeyDown(KeyCode.E) && cooking == false && this.tag == "oven")
            {
                StartCoroutine(TurnOn());
            }
            if (Input.GetKeyDown(KeyCode.E) && cooking == true && this.tag == "oven")
            {
                StartCoroutine(TurnOff());
            }
        }
    }
    private void Update()
    {
        if(cooking == true && coolDown == false && bScript.baseMudCount >= 1 && bScript.baseWoodCount >= 1)
        StartCoroutine(Cook());
    }

    private void OnTriggerExit(Collider other)
    {
        //popUp.text = "";
    }

    IEnumerator Cook()
    {
        Debug.Log("Cooking");
        coolDown = true;
        bScript.baseWoodCount = bScript.baseWoodCount -1;
        bScript.baseMudCount = bScript.baseMudCount - 1;
        yield return new WaitForSeconds(5f);
        GameObject a = Instantiate(brick) as GameObject;
        a.transform.position = (this.transform.position + new Vector3(0.0f, 3.0f, 0.0f));
        a.transform.parent = holder.transform;
        a.GetComponent<Rigidbody>().AddForce(Random.Range(-250, 250), 50, 100);
        Debug.Log("collision detected");
        coolDown = false;


    }
    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(.1f);
        cooking = true;
        Debug.Log("Oven On");

    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(.1f);
        cooking = false;
        Debug.Log("Oven Off");
    }

}