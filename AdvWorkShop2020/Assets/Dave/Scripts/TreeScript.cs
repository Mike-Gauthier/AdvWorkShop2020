using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private int counter = 0;
    public GameObject trunk, tree;
    public Transform player;
    private Rigidbody rb;
    private bool felled;
    public float force = 10;
    public GameObject wood, smokeParticle, splinterParticle;
    public GameObject holder, woodSpawn;

    private void Start()
    {
        rb = this.trunk.GetComponent<Rigidbody>();
        tree.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

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
            Instantiate(splinterParticle, trunk.transform.position, Quaternion.identity);
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
            a.transform.position = woodSpawn.transform.position + new Vector3(0,1,0);
            a.transform.parent = holder.transform;
            a.GetComponent<Rigidbody>().AddForce(Random.Range(-250, 250), 50, Random.Range(-250, 250));
        }
        Destroy(trunk);
        //play smoke effect
        Instantiate(smokeParticle, woodSpawn.transform.position, Quaternion.identity);
        

    }
}
