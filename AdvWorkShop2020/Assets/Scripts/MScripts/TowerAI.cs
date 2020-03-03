using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public Transform target;
    public float towerSight;
    public string enemyTag = "Enemy";
    //Enemy enemyHealth;
    //public int shotValue;

    public float fireRate = 1f;
    private float fireCD = 0f;

    public GameObject shotPrefab;
    public Transform shotSpot;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //enemyHealth = GetComponent<Enemy>.health();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float closest = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closest)
            {
                closest = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && closest <= towerSight)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        if (fireCD <= 0f)
        {
            Shoot();
            fireCD = 1f / fireRate;
        }
        fireCD -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerSight);
    }

    void Shoot()
    {
        GameObject shooter = (GameObject)Instantiate(shotPrefab, shotSpot.position, shotSpot.rotation);
        ShotScript shot = shooter.GetComponent<ShotScript>();

        if (shot != null)
            shot.Targeter(target);

        Debug.Log("Shoot");
    }

}
