using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCampScript : MonoBehaviour
{
    private Vector3 playerPos;
    public int speed, health = 3;
    public int seekingDistance;
    private bool inRange, attacking;

    void Update()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        //Getting player position
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;

        Seeking();

        if (inRange && !attacking)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(playerPos.x, transform.position.y, playerPos.z), speed * Time.deltaTime);
            this.transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        }
        else
        {
            //Dont Move
        }

    }

    void Seeking()
    {
        if(Mathf.Abs(playerPos.x - this.gameObject.transform.position.x) <= seekingDistance && (Mathf.Abs(playerPos.z - this.gameObject.transform.position.z) <= seekingDistance))
        {
            //Player is in range 
            inRange = true;
            //Running anim
        }
        else
        {
            inRange = false;
            //Idle anim
        }


    }
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            //Attack the player
            StartCoroutine(Attacking());
        }
    }


    public IEnumerator Attacking()
    {
        attacking = true;
        //Play attacking anim
        yield return new WaitForSeconds(0.7f);
        attacking = false;
        StopCoroutine(Attacking());
    }


}
