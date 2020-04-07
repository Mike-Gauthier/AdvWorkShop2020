using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public int counter = 0;
    public GameObject full;
    public GameObject half;
    public GameObject last;
    private bool noLongerFull = false;
    private bool noLongerHalf = false;
    private bool noLongerLast =false;
    public GameObject holder;
    public GameObject stoneParticle;
    public GameObject smokeParticle;

    public GameObject[] rockVariants;

    void Start()
    {
        
    }

    void Update()
    {
        if(counter == 2 && noLongerFull == false)
        {
            noLongerFull = true;
            full.SetActive(false);
        }

        if (counter == 4 && noLongerHalf == false)
        {
            noLongerHalf = true;
            half.SetActive(false);
        }

        if (counter == 6 && noLongerLast == false)
        {
            Instantiate(smokeParticle, this.transform.position, Quaternion.identity);
            noLongerLast = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "strike")
        {
            counter++;
            GameObject a = Instantiate(rockVariants[Random.Range(0,2)]) as GameObject;
            a.transform.position = (this.transform.position + new Vector3(0.0f, 3.0f, 0.0f));
            a.transform.parent = holder.transform;
            a.GetComponent<Rigidbody>().AddForce(Random.Range(-250 ,250), 50, Random.Range(-250 , 250));
            Debug.Log("collision detected");
            //drop stone resource
            //particle effect
            Instantiate(stoneParticle, this.transform.position, Quaternion.identity);
            //sound effect
        }
    }
}
