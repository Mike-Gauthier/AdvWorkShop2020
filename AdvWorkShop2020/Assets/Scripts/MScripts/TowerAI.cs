using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public Transform target;
    public float towerSight;
    //Enemy enemyHealth;
    //public int shotValue;

    private void Start()
    {
        //enemyHealth = GetComponent<Enemy>.health();
    }

    private void Update()
    {
        Ray towerRay = new Ray(transform.position, -transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.forward * towerSight);

        if (Physics.Raycast(towerRay, out hit, towerSight))
        {
            if(hit.collider.tag == "Seeker")
            {
                Debug.Log("Enemy Spotted");
                //AttackEnemy()
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerSight);
    }

}
