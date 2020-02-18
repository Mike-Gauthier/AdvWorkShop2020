using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private int counter = 0;
    public GameObject trunk;
    public Transform player;
    private Rigidbody rb;
    private bool felled;
    public float force = 10;
    public GameObject wood;
    public GameObject holder;

    private void Start()
    {
        rb = trunk.GetComponent<Rigidbody>();
    }
    void Update()
    {
      if(counter >= 2 && felled == false)
        {
            StartCoroutine(Timber());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "strike")
        {
            counter++;
            Debug.Log("collision detected");
            //particle effect
            //sound effect
        }
    }

    IEnumerator Timber()
    {
        felled = true;
        Debug.Log("falling");
        rb.AddForce(player.forward * force);
        yield return new WaitForSeconds(3);
        var woodCount = Random.Range(3, 5);
        for (var i = 0; i < woodCount; i++)
        {
            GameObject a = Instantiate(wood) as GameObject;
            a.transform.position = trunk.transform.position;
            a.transform.parent = holder.transform;
            a.GetComponent<Rigidbody>().AddForce(Random.Range(-250, 250), 50, Random.Range(-250, 250));
        }
        Destroy(trunk);
    }
}
